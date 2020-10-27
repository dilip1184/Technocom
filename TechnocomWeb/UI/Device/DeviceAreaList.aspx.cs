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
    public partial class DeviceAreaList : ctlPage
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
                    DeviceAreaEntity entity = new DeviceAreaEntity();
                    entity.OperationId = (int)OperationType.Delete;
                    entity.DeviceAreaId = Utility.GetInt(ViewState["DeviceAreaId"]);

                    OperationStatusEntity c = new DeviceRepository(SessionContext).UpdateDeviceArea(entity);

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
            ViewState["DeviceAreaId"] = null;

            txtDeviceAreaName.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/DeviceAreaList.aspx");
        }
        private void FillGrid()
        {
            var list = new DeviceRepository(SessionContext).GetDeviceAreaQuery();
            gridViewList.PageSize = 20;
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["DeviceAreaId"] = null;
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

                DeviceAreaEntity entity = new DeviceAreaEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.DeviceAreaId = Utility.GetInt(ViewState["DeviceAreaId"]);
                }

                entity.DeviceAreaName = txtDeviceAreaName.Text.Trim();

                OperationStatusEntity c = new DeviceRepository(SessionContext).UpdateDeviceArea(entity);

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
                ViewState["DeviceAreaId"] = Convert.ToString(arg[0]);
                long DeviceAreaId = Utility.GetLong(ViewState["DeviceAreaId"]);

                ViewState["Update"] = "Update";

                DeviceAreaEntity entity = new DeviceRepository(SessionContext).GetDeviceAreaById(DeviceAreaId).FirstOrDefault();

                txtDeviceAreaName.Text = entity.DeviceAreaName;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["DeviceAreaId"] = Convert.ToString(arg[0]);
                long DeviceAreaId = Utility.GetLong(ViewState["DeviceAreaId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Device Area?");
            }
        }
    }
}