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
    public partial class ZoneList : ctlPage
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
                    ZoneEntity entity = new ZoneEntity();
                    entity.OperationId = (int)OperationType.Delete;
                    entity.ZoneId = Utility.GetLong(ViewState["ZoneId"]);

                    OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateZone(entity);

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
            ViewState["ZoneId"] = null;

            ddlRegionSearch.ClearSelection();
            ddlRegion.ClearSelection();

            txtZoneNameSearch.Text = string.Empty;
            txtZoneName.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/ZoneList.aspx");
        }
        private void FillGrid()
        {
            var list = new ConfigrationRepository(SessionContext).GetAllZoneQuery(txtZoneNameSearch.Text, Utility.GetLong(ddlRegionSearch.SelectedValue));
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["ZoneId"] = null;
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

                ZoneEntity entity = new ZoneEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.ZoneId = Utility.GetLong(ViewState["ZoneId"]);
                }

                entity.ZoneName = txtZoneName.Text.Trim();
                entity.RegionId = Utility.GetLong(ddlRegion.SelectedValue);

                OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateZone(entity);

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
                ViewState["ZoneId"] = Convert.ToString(arg[0]);
                long ZoneId = Utility.GetLong(ViewState["ZoneId"]);

                ViewState["Update"] = "Update";

                LookupUtility.BindRegionLookup(ddlRegion, SessionContext);

                ZoneEntity entity = new ConfigrationRepository(SessionContext).GetZoneById(ZoneId).FirstOrDefault();

                Utility.SetLookupSelectedValue(ddlRegion, Convert.ToString(entity.RegionId));
                txtZoneName.Text = entity.ZoneName;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["ZoneId"] = Convert.ToString(arg[0]);
                long ZoneId = Utility.GetLong(ViewState["ZoneId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Zone?");
            }
        }
    }
}