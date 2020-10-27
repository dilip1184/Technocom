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
    public partial class HubList : ctlPage
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
                    HubEntity entity = new HubEntity();
                    entity.OperationId = (int)OperationType.Delete;
                    entity.HubId = Utility.GetLong(ViewState["HubId"]);

                    OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateHub(entity);

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
            ViewState["HubId"] = null;

            ddlRegionSearch.ClearSelection();
            ddlZoneSearch.ClearSelection();
            ddlBranchSearch.ClearSelection();

            ddlRegion.ClearSelection();
            ddlZone.ClearSelection();
            ddlBranch.ClearSelection();

            txtHubNameSearch.Text = string.Empty;
            txtHubName.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/HubList.aspx");
        }
        private void FillGrid()
        {
            var list = new ConfigrationRepository(SessionContext).GetAllHubQuery(txtHubNameSearch.Text, Utility.GetLong(ddlRegionSearch.SelectedValue), Utility.GetLong(ddlZoneSearch.SelectedValue), Utility.GetLong(ddlBranchSearch.SelectedValue));
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["HubId"] = null;
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
                ValidateBusinessData("G4");

                HubEntity entity = new HubEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.HubId = Utility.GetLong(ViewState["HubId"]);
                }

                entity.HubName = txtHubName.Text.Trim();
                entity.RegionId = Utility.GetLong(ddlRegion.SelectedValue);
                entity.ZoneId = Utility.GetLong(ddlZone.SelectedValue);
                entity.BranchId = Utility.GetLong(ddlBranch.SelectedValue);

                OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateHub(entity);

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
                ViewState["HubId"] = Convert.ToString(arg[0]);
                long HubId = Utility.GetLong(ViewState["HubId"]);

                ViewState["Update"] = "Update";

                LookupUtility.BindRegionLookup(ddlRegion, SessionContext);

                HubEntity entity = new ConfigrationRepository(SessionContext).GetHubById(HubId).FirstOrDefault();

                Utility.SetLookupSelectedValue(ddlRegion, Convert.ToString(entity.RegionId));
                ddlRegion_SelectedIndexChanged(null, null);

                Utility.SetLookupSelectedValue(ddlZone, Convert.ToString(entity.ZoneId));
                ddlZone_SelectedIndexChanged(null, null);

                Utility.SetLookupSelectedValue(ddlBranch, Convert.ToString(entity.BranchId));

                txtHubName.Text = entity.HubName;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["HubId"] = Convert.ToString(arg[0]);
                long HubId = Utility.GetLong(ViewState["HubId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Hub?");
            }
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindZoneLookup(ddlZone, SessionContext, Utility.GetLong(ddlRegion.SelectedValue));
            ddlZone.ClearSelection();
            ddlBranch.ClearSelection();
        }
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindBranchLookup(ddlBranch, SessionContext, Utility.GetLong(ddlZone.SelectedValue));
            ddlBranch.ClearSelection();
        }
        protected void ddlRegionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindZoneLookup(ddlZoneSearch, SessionContext, Utility.GetLong(ddlRegionSearch.SelectedValue));
            ddlZoneSearch.ClearSelection();
            ddlBranchSearch.ClearSelection();
        }
        protected void ddlZoneSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindBranchLookup(ddlBranchSearch, SessionContext, Utility.GetLong(ddlZoneSearch.SelectedValue));
            ddlBranchSearch.ClearSelection();
        }
    }
}