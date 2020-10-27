using TechnocomControl;
using TechnocomService;
using TechnocomShared.Configuration;
using TechnocomShared.Constants;
using TechnocomShared.Entities;
using TechnocomShared.Exceptions;
using TechnocomShared.Helpers;
using System;
using System.Web;
using System.Web.Security;

namespace TechnocomWeb
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            //ClientTarget = "uplevel";

            //if (!Request.IsLocal && !Request.IsSecureConnection)
            //{
            //    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
            //    Response.Redirect(redirectUrl);
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Request.IsLocal)
            {
                txtUserId.Text = "test";
                txtPassword.Attributes.Add("value", "test@123");
            }

            Session.Clear();
        }
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtUserId.Text.Trim().Equals(string.Empty) || txtPassword.Text.Trim().Equals(string.Empty)))
                {
                    lblmsg.Text = "Please enter User Id & Password";
                    return;
                }

                string userId = txtUserId.Text.Trim();
                string password = Server.HtmlDecode(txtPassword.Text);

                Login(userId, password);
            }
            catch (BaseException ex)
            {
                //lblmsg.Text = "Invalid User Name or Password.";
                lblmsg.Text = ex.Message;
            }
        }

        private void Login(string LoginId, string Password)
        {
            var response = new LogonService().LoginUser(LoginId, Password, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            Session.Timeout = AppConfigurationHelper.GetValue<int>(ConfigKeys.SessionTimeout);
            UserEntity user = DataHelper.GetObject<UserEntity>(response);
            var userSession = DataHelper.GetObject<UserSession>(response);

            SessionClass.LoginUserEntity = user;
            // SessionClass.FinancialYearId = new AcademicYearData().GetAcademicYear().Where(x => x.IsActive == true).FirstOrDefault().FinancialYearId;
           

            var userData = new UserSessionData { UserDisplayName = user.UserName, UserId = user.UserId, RoleId = user.RoleId, LoginTime = DateTime.Now, MenuList = DataHelper.GetListLevelZero<MenuEntity>(response) };

            var contextInfo = new ContextInfo { UserId = userSession.UserId, SessionId = userSession.Sessionid };

            Session[ctlMasterPage.ContextInfo] = contextInfo;
            Session[ctlMasterPage.UserSessionObject] = userData;

            FormsAuthentication.SetAuthCookie(Page.User.Identity.Name, false);

            // Response.Redirect("~/UI/AdminDashBoard.aspx", false);

            Response.Redirect("~/UI/DashBoard.aspx", false);
        }
    }
}