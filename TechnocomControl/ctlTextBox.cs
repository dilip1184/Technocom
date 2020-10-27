using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using TechnocomShared.Constants;
using TechnocomShared.Enums;
using TechnocomShared.Validation;

namespace TechnocomControl
{
    public class ctlTextBox : TextBox
    {
        /// <summary>
        /// Gets or sets the name of the meta source.
        /// </summary>
        /// <value>
        /// The name of the meta source.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public string MetaSourceName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the meta text field.
        /// </summary>
        /// <value>
        /// The meta text field.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public string MetaTextField
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the meta caption.
        /// </summary>
        /// <value>
        /// The meta caption.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public string MetaCaption
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [meta validation required].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [meta validation required]; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public bool MetaValidationRequired
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [meta mandatory].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [meta mandatory]; otherwise, <c>false</c>.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public bool MetaMandatory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the meta validation group.
        /// </summary>
        /// <value>
        /// The name of the meta validation group.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public string MetaValidationGroupName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the meta validation.
        /// </summary>
        /// <value>
        /// The type of the meta validation.
        /// </value>
        [Bindable(true)]
        [Localizable(true)]
        public ValidationTypes MetaValidationType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the meta validation error code.
        /// </summary>
        /// <value>
        /// The meta validation error code.
        /// </value>
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
            if (e.CommandName.Equals("Reset"))
            {
                Attributes.Add("style", "");
                return;
            }
            var strGroupName = (string) e.CommandArgument;
            if (Enabled)
            {
                if (((MetaValidationGroupName == strGroupName) || (strGroupName == "All"))
                                && MetaValidationRequired)
                {
                    //DoValidation

                    var errorMessage = CustomValidation.GenericValidation(
                                Text,
                                MetaCaption,
                                MetaMandatory,
                                MetaValidationType.ToString(),
                                MetaValidationErrorCode.ToString(),
                                "ctlTextBox");
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        sender.ErrorMessage = errorMessage;
                        Attributes.Add("style", StyleConstants.ValidationError);
                    }
                    else
                    {
                        Attributes.Add("style", "");
                    }
                }
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
            var data =  ctlPage.GetObjectFromObjectCollection((IList<object>)e.CommandArgument, MetaSourceName);
            if (data == null) return;
            var objType = data.GetType();
            var propInfo = objType.GetProperty(MetaTextField);
            Text = propInfo.GetValue(data, null).ToString();
        }
    }
}
