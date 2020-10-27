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
    public partial class SiteList : ctlPage
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
                    SiteEntity entity = new SiteEntity();
                    entity.OperationId = (int)OperationType.Delete;
                    entity.SiteId = Utility.GetLong(ViewState["SiteId"]);

                    OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateSite(entity);

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
            ViewState["SiteId"] = null;

            ddlRegionSearch.ClearSelection();
            ddlZoneSearch.ClearSelection();
            ddlBranchSearch.ClearSelection();
            ddlHubSearch.ClearSelection();
            ddlClusterSearch.ClearSelection();

            ddlRegion.ClearSelection();
            ddlZone.ClearSelection();
            ddlBranch.ClearSelection();
            ddlHub.ClearSelection();
            ddlCluster.ClearSelection();

            txtSiteNameSearch.Text = string.Empty;
            txtSiteName.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/SiteList.aspx");
        }
        private void FillGrid()
        {
            var list = new ConfigrationRepository(SessionContext).GetAllSiteQuery(txtSiteNameSearch.Text, Utility.GetLong(ddlRegionSearch.SelectedValue), Utility.GetLong(ddlZoneSearch.SelectedValue), Utility.GetLong(ddlBranchSearch.SelectedValue), Utility.GetLong(ddlHubSearch.SelectedValue), Utility.GetLong(ddlClusterSearch.SelectedValue));
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["SiteId"] = null;
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
                ValidateBusinessData("G5");
                ValidateBusinessData("G6");

                SiteEntity entity = new SiteEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.SiteId = Utility.GetLong(ViewState["SiteId"]);
                }

                entity.SiteName = txtSiteName.Text.Trim();
                entity.RegionId = Utility.GetLong(ddlRegion.SelectedValue);
                entity.ZoneId = Utility.GetLong(ddlZone.SelectedValue);
                entity.BranchId = Utility.GetLong(ddlBranch.SelectedValue);
                entity.HubId = Utility.GetLong(ddlHub.SelectedValue);
                entity.ClusterId = Utility.GetLong(ddlCluster.SelectedValue);

                OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateSite(entity);

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
                ViewState["SiteId"] = Convert.ToString(arg[0]);
                long SiteId = Utility.GetLong(ViewState["SiteId"]);

                ViewState["Update"] = "Update";

                LookupUtility.BindRegionLookup(ddlRegion, SessionContext);

                SiteEntity entity = new ConfigrationRepository(SessionContext).GetSiteById(SiteId).FirstOrDefault();

                txtSiteName.Text = entity.SiteName;

                Utility.SetLookupSelectedValue(ddlRegion, Convert.ToString(entity.RegionId));
                ddlRegion_SelectedIndexChanged(null, null);

                Utility.SetLookupSelectedValue(ddlZone, Convert.ToString(entity.ZoneId));
                ddlZone_SelectedIndexChanged(null, null);

                Utility.SetLookupSelectedValue(ddlBranch, Convert.ToString(entity.BranchId));
                ddlBranch_SelectedIndexChanged(null, null);

                Utility.SetLookupSelectedValue(ddlHub, Convert.ToString(entity.HubId));
                ddlHub_SelectedIndexChanged(null, null);

                Utility.SetLookupSelectedValue(ddlCluster, Convert.ToString(entity.ClusterId));

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["SiteId"] = Convert.ToString(arg[0]);
                long SiteId = Utility.GetLong(ViewState["SiteId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Site?");
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindZoneLookup(ddlZone, SessionContext, Utility.GetLong(ddlRegion.SelectedValue));
            ddlZone.ClearSelection();
            ddlBranch.ClearSelection();
            ddlHub.ClearSelection();
            ddlCluster.ClearSelection();
        }
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindBranchLookup(ddlBranch, SessionContext, Utility.GetLong(ddlZone.SelectedValue));
            ddlBranch.ClearSelection();
            ddlHub.ClearSelection();
            ddlCluster.ClearSelection();
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindHubLookup(ddlHub, SessionContext, Utility.GetLong(ddlZone.SelectedValue));
            ddlHub.ClearSelection();
            ddlCluster.ClearSelection();
        }
        protected void ddlHub_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindBranchLookup(ddlCluster, SessionContext, Utility.GetLong(ddlHub.SelectedValue));
            ddlCluster.ClearSelection();
        }
        protected void ddlRegionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindZoneLookup(ddlZoneSearch, SessionContext, Utility.GetLong(ddlRegionSearch.SelectedValue));
            ddlZoneSearch.ClearSelection();
            ddlBranchSearch.ClearSelection();
            ddlHubSearch.ClearSelection();
            ddlClusterSearch.ClearSelection();
        }
        protected void ddlZoneSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindBranchLookup(ddlBranchSearch, SessionContext, Utility.GetLong(ddlZoneSearch.SelectedValue));
            ddlBranchSearch.ClearSelection();
            ddlHubSearch.ClearSelection();
            ddlClusterSearch.ClearSelection();
        }
        protected void ddlBranchSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindHubLookup(ddlHubSearch, SessionContext, Utility.GetLong(ddlZoneSearch.SelectedValue));
            ddlHubSearch.ClearSelection();
            ddlClusterSearch.ClearSelection();
        }
        protected void ddlHubSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LookupUtility.BindBranchLookup(ddlClusterSearch, SessionContext, Utility.GetLong(ddlHubSearch.SelectedValue));
        }
    }
}