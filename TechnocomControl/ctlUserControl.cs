
namespace TechnocomControl
{
    public class ctlUserControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// Gets the parent page.
        /// </summary>
        protected ctlMasterPage MasterPage
        {
            get
            {
                if (ParentPage != null)
                    return ParentPage.MasterPage;
                return null;
            }
        }

        protected ctlPage ParentPage
        {
            get
            {
                if (Page != null)
                    return (ctlPage)Page;
                return null;
            }
        }
    }
}
