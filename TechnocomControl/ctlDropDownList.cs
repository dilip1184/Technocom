using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using TechnocomShared.Constants;
using TechnocomShared.Enums;
using TechnocomShared.Validation;

namespace TechnocomControl
{
    public class ctlDropDownList: DropDownList
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
            if (!MetaRemoveDefault)
            {
                Items.Add(new ListItem("--Select--", "0"));
                AppendDataBoundItems = true;
            }
            base.OnInit(e);
            var page = Page as ctlPage;
            if (page != null && MetaSourceName != string.Empty)
                page.DataLoadHandler += page_DataLoadHandler;
            if (page !=null)
                page.ValidationHandler += page_ValidationHandler;
        }

        void page_ValidationHandler(ctlPage sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null) return;
            
            if (e.CommandName.Equals("Reset"))
            {
                Attributes.Add("style", "");
                return;
            }

            var strGroupName = (string)e.CommandArgument;
            if (!Enabled) return;
            if (((MetaValidationGroupName != strGroupName) && (strGroupName != "All")) || !MetaValidationRequired)
                return;
            //DoValidation
            var errorMessage =
                CustomValidation.GenericValidation(
                    Text,
                    MetaCaption,
                    MetaMandatory,
                    MetaValidationType.ToString(),
                    MetaValidationErrorCode.ToString(),
                    "ctlDropDownList");
            if (!string.IsNullOrEmpty(errorMessage))
            {
                sender.ErrorMessage = errorMessage;
                Attributes.Add("style", StyleConstants.ValidationError);
            }
            else
                Attributes.Add("style", "");
        }

        void page_DataLoadHandler(ctlPage sender, CommandEventArgs e)
        {
            if (string.IsNullOrEmpty(MetaSourceName))
                return;
            var data = ctlPage.GetObjectFromObjectCollection((IList<object>)e.CommandArgument,MetaSourceName);
            DataSource = data;
            DataBind();
            SelectedIndex = -1;
        }

        public override void DataBind()
        {
            Items.Clear();
            base.DataBind();
            if (!MetaRemoveDefault)
            {
                Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
    }
}
