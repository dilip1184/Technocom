using TechnocomControl;
using TechnocomService;
using TechnocomShared.Entities;
using TechnocomShared.Exceptions;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnocomWeb.UI.HR
{
    public partial class UserLogin : ctlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            LookupUtility.BindUserRoleLookup(ddlRoleSearch, SessionContext);

            FillGrid();
        }
        private void ClearPageControl()
        {
            ViewState["Update"] = null;
            ViewState["UserId"] = null;

            txtUserNameSearch.Text = string.Empty;
            txtMobileNumberSearch.Text = string.Empty;
            txtNICNoSearch.Text = string.Empty;
            ddlRoleSearch.ClearSelection();

            lblEmployeeName.Text = string.Empty;
            txtLoginId.Text = string.Empty;
            txtPassword.Text = string.Empty;

            chkIsActivateLogin.Checked = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/HR/UserLogin.aspx");
        }
        private void FillGrid()
        {
            var list = new HRRepository(SessionContext).GetEmployeeInformationList(txtUserNameSearch.Text, txtMobileNumberSearch.Text, txtNICNoSearch.Text, Utility.GetInt(ddlRoleSearch.SelectedValue));
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Update"] = null;
            ViewState["UserId"] = null;
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateBusinessData("G1");
                ValidateBusinessData("G2");

                UserEntity entity = new UserEntity();

                entity.UserId = Utility.GetInt(ViewState["UserId"]);

                entity.LoginId = txtLoginId.Text.Trim();
                entity.UserPassword = txtPassword.Text.Trim();

                //entity.IsActivateLogin = chkIsActivateLogin.Checked;
                //entity.CreatedBy = SessionClass.LoginUserEntity.UserId;

                OperationStatusEntity c = new HRRepository(SessionContext).UpdateUserLoginUpdate(entity);
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
                ViewState["UserId"] = Convert.ToString(arg[0]);
                long UserId = Utility.GetLong(ViewState["UserId"]);

                ViewState["Update"] = "Update";

                UserEntity entity = new HRRepository(SessionContext).GetEmployeeInformationById(UserId);

                lblEmployeeName.Text = entity.UserName;
                txtLoginId.Text = entity.LoginId;
                txtPassword.Text = entity.UserPassword;

                //chkIsActivateLogin.Checked = entity.IsActivateLogin;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
        }
    }
}