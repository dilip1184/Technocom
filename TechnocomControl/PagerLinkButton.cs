using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnocomControl
{
    public class PagerLinkButton : LinkButton
    {
        public PagerLinkButton(IPostBackContainer container)
        {
            _container = container;
        }

        public void EnableCallback(string argument)
        {
            _enableCallback = true;
            _callbackArgument = argument;
        }

        public override bool CausesValidation
        {
            get { return false; }
            set { throw new ApplicationException("Cannot set validation on pager buttons"); }
        }
        public override string CssClass
        {
            get
            {
                return base.CssClass;
            }
            set
            {
                base.CssClass = "paging-box";
            }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            SetCallbackProperties();
            base.Render(writer);
            //base.CssClass = "paging-box";
        }

        private void SetCallbackProperties()
        {
            if (_enableCallback)
            {
                var container = _container as ICallbackContainer;
                if (container != null)
                {
                    var callbackScript = container.GetCallbackScript(this, _callbackArgument);
                    if (!string.IsNullOrEmpty(callbackScript)) OnClientClick = callbackScript;
                }
            }
            base.CssClass = "paging-box";
        }

        private readonly IPostBackContainer _container;
        private bool _enableCallback;
        private string _callbackArgument;
    }
}