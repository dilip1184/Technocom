using System.ComponentModel;
using TechnocomShared.Enums;

namespace TechnocomControl
{
    public class ctlListBox : System.Web.UI.WebControls.ListBox
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
        
    }
}
