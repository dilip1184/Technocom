using System;
using System.Web.UI.WebControls;

namespace TechnocomControl
{
    public class ctlRadioButtonList : RadioButtonList
    {
        /// <summary>
        /// Handles the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var page = Page as ctlPage;
            if (page != null)
                page.DataLoadHandler += page_DataLoadHandler;
        }

        /// <summary>
        /// Data load handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        void page_DataLoadHandler(ctlPage sender, CommandEventArgs e)
        {
            ClearSelection();
        }
    }
}
