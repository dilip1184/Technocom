using TechnocomControl;
using TechnocomService;
using TechnocomShared.Entities;
using TechnocomShared.Exceptions;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace TechnocomWeb.UI.HR
{
    public partial class RoleManagement : ctlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage.YesClickHandler += MasterPage_YesClickHandler;

            if (Page.IsPostBack) return;

            FillGrid();
        }
        protected void MasterPage_YesClickHandler(object sender, EventArgs e)
        {
            if (ViewState["Delete"] != null)
            {
                try
                {
                    RoleManagementEntity entity = new RoleManagementEntity();
                    entity.OperationId = 3;
                    entity.RoleId = Utility.GetInt(ViewState["RoleId"]);

                    OperationStatusEntity c = new HRRepository(SessionContext).ManageRole(entity);
                    if (c.StatusResult == true)
                    {
                        ShowInfoMessage(c.InfoMessage);
                        FillGrid();
                    }
                    else
                    {
                        ShowErrorMessage(c.InfoMessage);
                    }
                }
                catch (BaseException bex)
                {
                    ShowErrorMessage(bex.Message);
                }
            }
        }
        private void ClearPageControl()
        {
            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["RoleId"] = null;

            txtRoleName.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/HR/RoleManagement.aspx");
        }
        private void FillGrid()
        {
            var list = new HRRepository(SessionContext).GetRoleList();
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["RoleId"] = null;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ShowErrorMessage(string.Empty);
                FillGrid();
            }
            catch (BaseException be)
            {
                ShowErrorMessage(be.DisplayMessage);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearPageControl();

            ViewState["Add"] = "Add";

            var list = new HRRepository(SessionContext).GetMenuListByRoleId(Utility.GetInt(ViewState["RoleId"])).Where(x => x.ParentId == 0 && x.NavigationId != 1).ToList();

            gvMenuList.DataSource = list;
            gvMenuList.DataBind();

            DIVList.Visible = false;
            DIVDetail.Visible = true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateBusinessData("G1");

                RoleManagementEntity entity = new RoleManagementEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.RoleId = Utility.GetInt(ViewState["RoleId"]);
                }

                entity.RoleName = txtRoleName.Text.Trim();

                OperationStatusEntity c = new HRRepository(SessionContext).ManageRole(entity);
                if (c.StatusResult == true)
                {
                    foreach (RepeaterItem Item in gvMenuList.Items)
                    {
                        string NavigationIds = Utility.GetCheckBoxListSelectedValue(((CheckBoxList)Item.FindControl("chkChildMenuList")));

                        if (!string.IsNullOrWhiteSpace(NavigationIds))
                        {
                            new HRRepository(SessionContext).UpdateMenuRoleDetail((int)c.ResultId, Utility.GetInt(((Label)Item.FindControl("lblParentId")).Text),
                                                                                  NavigationIds, SessionClass.LoginUserEntity.UserId);
                        }
                    }

                    ShowInfoMessage(c.InfoMessage);
                    ClearPageControl();
                    FillGrid();
                }
                else
                {
                    ShowErrorMessage(c.InfoMessage);
                }
            }
            catch (ValidationException ex)
            {
                ShowErrorMessage(ex.Message);
            }

            catch (BaseException be)
            {
                ShowErrorMessage(be.DisplayMessage);
            }
        }
        protected void gridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            if (e.CommandName == "EditRow")
            {
                ViewState["RoleId"] = Convert.ToString(arg[0]);
                int RoleId = Utility.GetInt(ViewState["RoleId"]);

                ViewState["Update"] = "Update";

                RoleManagementEntity entity = new HRRepository(SessionContext).GetRoleById(RoleId);
                txtRoleName.Text = entity.RoleName.Trim();

                var list = new HRRepository(SessionContext).GetMenuListByRoleId(Utility.GetInt(ViewState["RoleId"])).Where(x => x.ParentId == 0 && x.NavigationId != 1).ToList();

                gvMenuList.DataSource = list;
                gvMenuList.DataBind();

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["RoleId"] = Convert.ToString(arg[0]);
                int RoleId = Utility.GetInt(ViewState["RoleId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Role and all associated Menu items?");
            }
        }
        protected void gvMenuList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int ParentId = Utility.GetInt(((Label)e.Item.FindControl("lblParentId")).Text);
                CheckBoxList childList = e.Item.FindControl("chkChildMenuList") as CheckBoxList;
                if (childList != null)
                {
                    var list = new HRRepository(SessionContext).GetMenuListByRoleId(Utility.GetInt(ViewState["RoleId"])).Where(x => x.ParentId == ParentId).ToList();

                    childList.DataSource = list;
                    childList.DataTextField = "MenuName";
                    childList.DataValueField = "NavigationId";
                    childList.DataBind();

                    foreach (ListItem listItem in childList.Items)
                    {
                        foreach (MenuItemEntity p in list)
                        {
                            if (p.IsChecked == true && p.NavigationId == Utility.GetInt(listItem.Value))
                            {
                                listItem.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}