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
    public partial class DeviceList : ctlPage
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
                    DeviceEntity entity = new DeviceEntity();
                    entity.OperationId = (int)OperationType.Delete;
                    entity.DeviceId = Utility.GetInt(ViewState["DeviceId"]);

                    OperationStatusEntity c = new DeviceRepository(SessionContext).UpdateDevice(entity);

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
            ViewState["DeviceId"] = null;

            txtDeviceNameSearch.Text = string.Empty;
            lblSN.Text = string.Empty;
            txtDeviceAliasName.Text = string.Empty;
            lblAttLogStamp.Text = string.Empty;
            lblOnlineStatus.Text = string.Empty;
            lblIPAddress.Text = string.Empty;
            lblServerPort.Text = string.Empty;

            ddlIsActive.ClearSelection();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/DeviceList.aspx");
        }
        private void FillGrid()
        {
            var list = new DeviceRepository(SessionContext).GetAllDeviceQuery(txtDeviceNameSearch.Text);
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["Delete"] = null;
            ViewState["DeviceId"] = null;
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

                DeviceEntity entity = new DeviceEntity();

                if (Convert.ToString(ViewState["Add"]) == "Add")
                {
                    entity.OperationId = 1;
                }
                else if (Convert.ToString(ViewState["Update"]) == "Update")
                {
                    entity.OperationId = 2;
                    entity.DeviceId = Utility.GetInt(ViewState["DeviceId"]);
                }

                entity.DeviceAliasName = txtDeviceAliasName.Text;
                entity.AttLogStamp = lblAttLogStamp.Text;

                bool IsActive = false;
                if (Utility.GetInt(ddlIsActive.SelectedValue) == 1)
                {
                    IsActive = true;
                }
                else
                {
                    IsActive = false;
                }

                new DeviceRepository(SessionContext).UpdateDeviceStatus(entity.DeviceId, entity.DeviceAliasName, IsActive);

                ShowInfoMessage("Device updated successfully.");
                ClearPageControl();
                FillGrid();
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
                ViewState["DeviceId"] = Convert.ToString(arg[0]);
                long DeviceId = Utility.GetLong(ViewState["DeviceId"]);

                ViewState["Update"] = "Update";

                LookupUtility.BindStatusTypeLookup(ddlIsActive, SessionContext);

                DeviceEntity entity = new DeviceRepository(SessionContext).GetAllDeviceQuery(string.Empty).Where(x => x.DeviceId == DeviceId).FirstOrDefault();

                if (entity.IsActive == true)
                {
                    ddlIsActive.SelectedValue = "1";
                }
                else
                {
                    ddlIsActive.SelectedValue = "2";
                }

                lblSN.Text = entity.SN;
                txtDeviceAliasName.Text = entity.DeviceAliasName;
                lblAttLogStamp.Text = entity.AttLogStamp;
                lblOnlineStatus.Text = entity.MachineStatus;
                lblIPAddress.Text = entity.IPAddress;
                lblServerPort.Text = entity.ServerPort;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["DeviceId"] = Convert.ToString(arg[0]);
                long DeviceId = Utility.GetLong(ViewState["DeviceId"]);

                ViewState["Delete"] = "Delete";

                ShowYesNoPopup("Are you sure you want to delete this Device?");
            }
        }
    }
}