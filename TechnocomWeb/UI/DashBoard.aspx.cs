using TechnocomShared.Entities;
using TechnocomService;
using TechnocomControl;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using TechnocomShared.Exceptions;
using System.Collections.Generic;

namespace TechnocomWeb.UI
{
    public partial class DashBoard : ctlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ValidateBusinessData("G1");

        //        List<ListItem> keys = ddlTestLookup.GetMultiSelectedData().ToList();

        //    }
        //    catch (ValidationException ex)
        //    {
        //        ShowErrorMessage(ex.Message);
        //    }

        //    catch (BaseException be)
        //    {
        //        ShowErrorMessage(be.DisplayMessage);
        //    }
        //}
        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    NavigateTo("~/UI/DashBoard.aspx");
        //}

        //protected void ddlTestLookup_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}