using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using TechnocomShared.Constants;
using TechnocomShared.Enums;
using TechnocomShared.Validation;

namespace TechnocomControl
{   
    [DefaultProperty("Text")]
    public class ctlTextArea : TextBox
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
        public string MetaTextField
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
        

        public override TextBoxMode TextMode
        {
            get
            {
                return TextBoxMode.MultiLine;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (MaxLength > 0)
            {
                Attributes.Add("onkeypress","LimitInput(this)");
                Attributes.Add("onbeforepaste", "doBeforePaste(this)");
                Attributes.Add("onpaste", "doPaste(this)");
                Attributes.Add("onmousemove", "LimitInput(this)");
                Attributes.Add("maxLength",MaxLength.ToString());
            }
            base.OnPreRender(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var page = Page as ctlPage;
            if (page == null) return;
            page.DataLoadHandler += page_DataLoadHandler;
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
            if (((MetaValidationGroupName == strGroupName) || (strGroupName == "All"))
                && MetaValidationRequired)
            {
                //DoValidation
                var errorMessage =
                    CustomValidation.GenericValidation(
                        Text,
                        MetaCaption,
                        MetaMandatory,
                        MetaValidationType.ToString(),
                        MetaValidationErrorCode.ToString(),
                        "ctlTextArea");
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    sender.ErrorMessage = errorMessage;
                    Attributes.Add("style", StyleConstants.ValidationError);
                }
                else
                    Attributes.Add("style", "");
            }
        }

        /// <summary>
        /// Page_s the data load handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        void page_DataLoadHandler(ctlPage sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null) return;
            var data = ctlPage.GetObjectFromObjectCollection((IList<object>)e.CommandArgument, MetaSourceName);
            if (data != null)
            {
                var objType = data.GetType();
                var propInfo = objType.GetProperty(MetaTextField);
                Text = propInfo.GetValue(data, null).ToString();
            }
        }
    }
}
