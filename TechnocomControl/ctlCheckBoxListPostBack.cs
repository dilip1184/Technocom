using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TechnocomShared.Entities;
using TechnocomShared.Constants;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;
using System.Web;
using TechnocomShared.Validation;

namespace TechnocomControl
{
    public class ctlCheckBoxListPostBack : CheckBoxList
    {

        [Bindable(true)]
        [Localizable(true)]
        public string MetaSourceName
        {
            get;
            set;
        }

        [Bindable(true)]
        [Localizable(true)]
        public bool MetaRemoveDefault
        {
            get;
            set;
        }

        [Bindable(true)]
        [Localizable(true)]
        public string MetaCaption
        {
            get;
            set;
        }

        [Bindable(true)]
        [Localizable(true)]
        public bool MetaValidationRequired
        {
            get;
            set;
        }

        [Bindable(true)]
        [Localizable(true)]
        public bool MetaMandatory
        {
            get;
            set;
        }

        [Bindable(true)]
        [Localizable(true)]
        public string MetaValidationGroupName
        {
            get;
            set;
        }

        [Bindable(true)]
        [Localizable(true)]
        public ValidationTypes MetaValidationType
        {
            get;
            set;
        }

        [Bindable(true)]
        [Localizable(true)]
        public ValidationErrorCodes MetaValidationErrorCode
        {
            get;
            set;
        }
        protected override void OnInit(EventArgs e)
        {
            this.Items.Insert(0, new ListItem() { Text = "<b>Select All</b>", Value = "-1" });
            this.Items[0].Attributes.Add("onclick", "CheckAll(this);");

            base.OnInit(e);

            var page = Page as ctlPage;

            if (page != null && MetaSourceName != string.Empty)
                page.DataLoadHandler += page_DataLoadHandler;

            //if (page != null)
            //    page.ValidationHandler += page_ValidationHandler;
        }
        //void page_ValidationHandler(ctlPage sender, CommandEventArgs e)
        //{
        //    if (e.CommandArgument == null) return;

        //    if (e.CommandName.Equals("Reset"))
        //    {
        //        Attributes.Add("style", "");
        //        return;
        //    }

        //    var strGroupName = (string)e.CommandArgument;
        //    if (!Enabled) return;
        //    if (((MetaValidationGroupName != strGroupName) && (strGroupName != "All")) || !MetaValidationRequired)
        //        return;
        //    //DoValidation
        //    var errorMessage =
        //        CustomValidation.GenericValidation(
        //            GetCheckBoxListSelectedData(),
        //            MetaCaption,
        //            MetaMandatory,
        //            MetaValidationType.ToString(),
        //            MetaValidationErrorCode.ToString(),
        //            "ctlCheckBoxList");
        //    if (!string.IsNullOrEmpty(errorMessage))
        //    {
        //        sender.ErrorMessage = errorMessage;
        //        Attributes.Add("style", StyleConstants.ValidationError);
        //    }
        //    else
        //        Attributes.Add("style", "");
        //}
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            string value = string.Empty;

            string result = HttpContext.Current.Request.Form["__EVENTTARGET"];

            string[] checkedBox = result.Split('$'); ;

            int index = int.Parse(checkedBox[checkedBox.Length - 1]);

            if (this.Items[index].Value == "-1")
            {
                for (var i = 1; i < Items.Count; i++)
                {
                    Items[i].Selected = this.Items[index].Selected;
                }
            }
            else
            {
                if (this.Items.Cast<ListItem>().Where(x => x.Selected && x.Value != "-1").Count() == this.Items.Count - 1)
                {
                    this.Items[0].Selected = true;
                }
                else
                {
                    this.Items[0].Selected = false;
                }
            }
            base.OnSelectedIndexChanged(e);//raise the event for the page handler
        }
        void page_DataLoadHandler(ctlPage sender, CommandEventArgs e)
        {
            if (string.IsNullOrEmpty(MetaSourceName))
                return;
            var data = ctlPage.GetObjectFromObjectCollection((IList<object>)e.CommandArgument, MetaSourceName);
            DataSource = data;
            DataBind();
            SelectedIndex = -1;
        }

        public override void DataBind()
        {
            Items.Clear();
            base.DataBind();
            this.Items.Insert(0, new ListItem() { Text = "<b>Select All</b>", Value = "-1" });
            this.Items[0].Attributes.Add("onclick", "CheckAll(this);");
        }

        [BrowsableAttribute(true)]
        [DefaultValue("record")]
        public string NoCheckCaption
        {
            get;
            set;
        }

        //public string GetCheckBoxListSelectedData()
        //{
        //    string data = string.Empty;
        //    List<ListItem> response = new List<ListItem>();

        //    for (var i = 0; i < Items.Count; i++)
        //    {
        //        if (Items[i].Selected == true)
        //        {
        //            SelectedIndex = i;
        //            response.Add(Items[i]);
        //        }
        //    }

        //    if (response.Count > 0)
        //    {
        //        data = string.Join(",", response.Select(item => item.Value));
        //    }

        //    return data;
        //}

        //public override void RenderControl(HtmlTextWriter output)
        //{
        //    this.Items.Insert(0, new ListItem() { Text = "Select All", Value = "-1" });
        //    //output.WriteBeginTag("div");
        //    //output.WriteAttribute("style", "width:" + this.Width +
        //    //                               ";background-color: #3d80df;color:White;font-family:" +
        //    //                               this.Font.Name + ";font-size:" + this.Font.Size);
        //    //output.Write(">");
        //    //var withcheckBox = String.Format("<input type='checkbox' name='selectCheckBox Id='selectCheckBox'>");
        //    //output.Write(withcheckBox);

        //    //output.WriteEndTag("div");
        //    //output.WriteBeginTag("div");
        //    //output.WriteAttribute("style", "height:200px;width:" + this.Width + ";overflow-y:auto; overflow-x:hidden;");
        //    //output.Write(">");
        //    base.RenderControl(output);
        //    //output.WriteEndTag("div");
        //}
    }
}