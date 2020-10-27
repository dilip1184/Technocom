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

namespace TechnocomWeb.UI.Configuration
{
    public partial class BranchList : ctlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage.YesClickHandler += MasterPage_YesClickHandler;

            if (Page.IsPostBack) return;
            
            LookupUtility.BindRegionLookup(ddlRegionSearch, SessionContext);

            FillGrid();
        }
        protected void MasterPage_YesClickHandler(object sender, EventArgs e)
        {
            if (ViewState["Delete"] != null)
            {
                try
                {
                    BranchEntity entity = new BranchEntity();
                    entity.OperationId = (int)OperationType.Delete;
                    entity.BranchId = Utility.GetLong(ViewState["BranchId"]);

                    OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateBranch(entity);

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
            ViewState["BranchId"] = null;

            ddlRegionSearch.ClearSelection();
            ddlZoneSearch.ClearSelection();

            ddlRegion.ClearSelection();
            ddlZone.ClearSelection();

            txtBranchNameSearch.Text = string.Empty;
            txtBranchName.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/BranchList.aspx");
        }
        private void FillGrid()
        {
            var list = new ConfigrationRepository(SessionContext).GetAllBranchQuery(txtBranchNameSearch.Text, Utility.GetLong(ddlRegionSearch.SelectedValue), Utility.GetLong(ddlZoneSearch.SelectedValue));
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["BranchId"] = null;
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
            LookupUtility.BindRegionLookup(ddlRegion, SessionContext);

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
                ValidateBusinessData("G2");
                ValidateBusinessData("G3");

                BranchEntity entity = new BranchEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.BranchId = Utility.GetLong(ViewState["BranchId"]);
                }

                entity.BranchName = txtBranchName.Text.Trim();
                entity.RegionId = Utility.GetLong(ddlRegion.SelectedValue);
                entity.ZoneId = Utility.GetLong(ddlZone.SelectedValue);

                OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateBranch(entity);

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
                ViewState["BranchId"] = Convert.ToString(arg[0]);
                long BranchId = Utility.GetLong(ViewState["BranchId"]);

                ViewState["Update"] = "Update";

                LookupUtility.BindRegionLookup(ddlRegion, SessionContext);

                BranchEntity entity = new ConfigrationRepository(SessionContext).GetBranchById(BranchId).FirstOrDefault();

                Utility.SetLookupSelectedValue(ddlRegion, Convert.ToString(entity.RegionId));
                ddlRegion_SelectedIndexChanged(null, null);

                Utility.SetLookupSelectedValue(ddlZone, Convert.ToString(entity.ZoneId));
                txtBranchName.Text = entity.BranchName;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["BranchId"] = Convert.ToString(arg[0]);
                long BranchId = Utility.GetLong(ViewState["BranchId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Branch?");
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindZoneLookup(ddlZone, SessionContext, Utility.GetLong(ddlRegion.SelectedValue));
            ddlZoneSearch.ClearSelection();
        }
        protected void ddlRegionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindZoneLookup(ddlZoneSearch, SessionContext, Utility.GetLong(ddlRegionSearch.SelectedValue));
            ddlZoneSearch.ClearSelection();
        }
    }
}