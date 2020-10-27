using TechnocomControl;
using TechnocomService;
using TechnocomShared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnocomWeb.UI.HR
{
    public partial class ChangePassword : ctlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            lblEmployeeName.Text = SessionClass.LoginUserEntity.UserName.Trim();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            NavigateToHome();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateBusinessData("G1");
                ValidateBusinessData("G2");

                if (!String.Equals(txtNewPassword.Text.Trim(), txtConfirmPassword.Text.Trim(), StringComparison.CurrentCulture))
                {
                    ShowErrorMessage("Confirm Password does not match.");
                    txtConfirmPassword.Text = string.Empty;
                    txtConfirmPassword.Focus();
                    return;
                }
                
                string c = new HRRepository(SessionContext).UpdateUserPassword(txtNewPassword.Text.Trim(), SessionClass.LoginUserEntity.UserId);
                if (c == "success")
                {
                    ShowInfoMessage("Password changed successfully.");
                    txtNewPassword.Text = string.Empty;
                    txtConfirmPassword.Text = string.Empty;
                }
                else
                {
                    ShowErrorMessage("Error in change password.");
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
    }
}