using System.Collections;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using TechnocomShared.Entities;

namespace TechnocomControl
{
    public class ctlBreadCrumb : System.Web.UI.Control
    {
        protected override void CreateChildControls()
        {
            var parentPage = (ctlPage)Page;

            if (parentPage.IsPostBack) { return; }

            Controls.Add(new Literal { Text = ProcessBreadCrumb(parentPage) });
        }

        private static string ProcessBreadCrumb(ctlPage parentPage)
        {
            var screenList = parentPage.MasterPage.GetMenu(parentPage.UserData.RoleId);
            var currentScreen = screenList.FirstOrDefault(x => x.NavigationId.Equals(parentPage.MasterPage.NavigationId));

            if (currentScreen == null) { return string.Empty; }

            var breadCrumb = new Stack();
            breadCrumb.Push(currentScreen);

            
            while (currentScreen.ParentId != 0)
            {
                currentScreen = screenList.First(x => x.NavigationId.Equals(currentScreen.ParentId));
                breadCrumb.Push(currentScreen);
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<li>");
            var totalCrumbs = breadCrumb.Count;
            for (var i = 1; i <= totalCrumbs; i++)
            {
                var screenName = (MenuEntity)breadCrumb.Pop();

                if (i == totalCrumbs)
                {
                    stringBuilder.Append("<span>" + screenName.MenuName + "</span>");
                }
                else
                {
                    stringBuilder.Append(screenName.MenuName);
                    stringBuilder.Append("<i class='fa fa-circle'></i>");
                }
            }

            stringBuilder.Append("</li>");
            return stringBuilder.ToString();
        }
    }
}