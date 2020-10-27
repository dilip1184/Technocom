using TechnocomShared.Entities;
using TechnocomService;
using TechnocomShared.Configuration;
using TechnocomShared.Constants;
using System;
using System.Linq;

namespace TechnocomWeb
{
    public partial class CommonError : System.Web.UI.Page
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

        }
    }
}