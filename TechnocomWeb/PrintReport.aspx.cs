using System;

namespace TechnocomWeb
{
    public partial class PrintReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["ReportName"]))
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
    }
}