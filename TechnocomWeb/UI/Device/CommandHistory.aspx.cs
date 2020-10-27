using TechnocomControl;
using TechnocomService;
using TechnocomShared.Entities;
using TechnocomShared.Exceptions;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using TechnocomShared.Enums;

namespace TechnocomWeb.UI.Device
{
    public partial class CommandHistory : ctlPage
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
                    RegionEntity entity = new RegionEntity();
                    entity.OperationId = (int)OperationType.Delete;
                    entity.RegionId = Utility.GetLong(ViewState["RegionId"]);

                    OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateRegion(entity);

                    if (c.StatusResult == true)
                    {
                        ShowInfoMessage(c.InfoMessage);
                        ClearPageControl();
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
            ViewState["RegionId"] = null;

            txtRegionNameSearch.Text = string.Empty;
            txtRegionName.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/RegionList.aspx");
        }
        private void FillGrid()
        {
            var list = new ConfigrationRepository(SessionContext).GetAllRegionQuery(txtRegionNameSearch.Text);
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["RegionId"] = null;
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

            DIVList.Visible = false;
            DIVDetail.Visible = true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateBusinessData("G1");

                RegionEntity entity = new RegionEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.RegionId = Utility.GetLong(ViewState["RegionId"]);
                }

                entity.RegionName = txtRegionName.Text.Trim();

                OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateRegion(entity);

                if (c.StatusResult == true)
                {
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
                ViewState["RegionId"] = Convert.ToString(arg[0]);
                long RegionId = Utility.GetLong(ViewState["RegionId"]);

                ViewState["Update"] = "Update";

                RegionEntity entity = new ConfigrationRepository(SessionContext).GetRegionById(RegionId).FirstOrDefault();

                txtRegionName.Text = entity.RegionName;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["RegionId"] = Convert.ToString(arg[0]);
                long RegionId = Utility.GetLong(ViewState["RegionId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Region?");
            }
        }
    }
}