using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using TechnocomShared.Constants;
using TechnocomShared.Enums;
using TechnocomShared.Validation;

namespace TechnocomControl
{
    public class ctlRadioButton : RadioButton
    {
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
            var strGroupName = (string)e.CommandArgument;
            if (((MetaValidationGroupName == strGroupName) || (strGroupName == "All"))
                            && MetaValidationRequired)
            {
                //DoValidation
                var errorMessage = 
                                        CustomValidation.GenericValidation(
                                        Checked.ToString(),
                                        MetaCaption,
                                        MetaMandatory,
                                        MetaValidationType.ToString(),
                                        MetaValidationErrorCode.ToString(),
                                        "ctlRadioButton");
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
            //below code commented as no MetaSourceName exists as property passed MetaCaption instead - PS
            //var data = sender.GetObjectFromObjectCollection((IList<object>)e.CommandArgument, MetaSourceName);
            var data = ctlPage.GetObjectFromObjectCollection((IList<object>)e.CommandArgument, MetaCaption);
            if (data == null) return;
            var objType = data.GetType();
            //below code commented as no MetaTextField exists as property passed MetaCaption instead - PS
            //PropertyInfo propInfo = objType.GetProperty(MetaTextField);
            var propInfo = objType.GetProperty(MetaCaption);
            Text = propInfo.GetValue(data, null).ToString();
        }
    }
}
