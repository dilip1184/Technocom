using TechnocomService;
using TechnocomShared.Exceptions;
using TechnocomShared.Logging;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace TechnocomWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RouteTable.Routes.Ignore("{resource}.axd");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var objErr = Server.GetLastError().GetBaseException();

                var err = "Error Caught in Application_Error event\n" +
                        "Error in: " + Request.Url +
                        "\nError Message:" + objErr.Message +
                        "\nStack Trace:" + objErr.StackTrace;

                string createdBy = string.Empty;
                if (SessionClass.LoginUserEntity != null)
                {
                    createdBy = SessionClass.LoginUserEntity.LoginId;
                }

               // new WorkoutData().InsertError("Error in: " + Request.Url.ToString() + " Error Message:" + objErr.Message, createdBy);

                LogWriter.GetLogWriter().Exception(err);
                Server.ClearError();

                if (objErr.GetType().Equals(typeof(SessionTimeoutException)))
                {
                    //FormsAuthentication.SignOut();
                    //Session.Abandon();
                    Response.Redirect("~/SessionTimeout.aspx", false);
                }
                else
                {
                    Response.Redirect("~/CommonError.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/CommonError.aspx", false);
            }

        }
    }
}