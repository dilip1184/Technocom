using TechnocomControl;
using TechnocomService;
using TechnocomShared.Caching;
using TechnocomShared.Entities;
using TechnocomShared.Enums;
using TechnocomShared.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TechnocomWeb.UI.Shared
{
    public partial class _default : ctlMasterPage
    {
        readonly CacheWriter _objCacheWriter = CacheWriter.GetCacheManager();
        public override event OnOkButtonClick OkClickHandler;
        public override event OnYesButtonClick YesClickHandler;
        public override event OnNoButtonClick NoClickHandler;
        public override event OnPopupButtonClick PopupClickHandler;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            if (IsPostBack) return;

            GetMenu();
        }

        private void GetMenu()
        {
            if (ViewState["MenuList"] == null)
            {
                var list = new LogonService().GetMenuItems(SessionClass.LoginUserEntity.RoleId);
                ViewState["MenuList"] = list;
                RepeaterMenu.DataSource = list.Where(x => x.ParentId == 0).ToList();
                RepeaterMenu.DataBind();
            }
        }
        protected void RepeaterMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int NavigationId = Utility.GetInt(((Label)e.Item.FindControl("lblNavigationId")).Text);

                Repeater subMenu = e.Item.FindControl("RepeaterSubMenu") as Repeater;

                if (subMenu != null)
                {
                    List<MenuEntity> list = ((List<MenuEntity>)ViewState["MenuList"]);

                    subMenu.DataSource = list.Where(x => x.ParentId == NavigationId).ToList();
                    subMenu.DataBind();
                }
            }
        }
        protected void RepeaterMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string URLPath = ((Label)e.Item.FindControl("lblMenuURL")).Text;

            var screens = ((List<MenuEntity>)ViewState["MenuList"]);
            try
            {
                var screen = screens.First(x => x.URLPath != null && URLPath.Contains(x.URLPath));
                HiddenScreenId = screen.NavigationId;
            }
            catch (InvalidOperationException)
            {
                LogWriter.GetLogWriter().Debug("--------------Unable to find NavigateTO entry for----" + URLPath);
            }

            Response.Redirect(URLPath, false);
        }
        protected void RepeaterSubMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string URLPath = ((Label)e.Item.FindControl("lblSubMenuURL")).Text;

            var screens = ((List<MenuEntity>)ViewState["MenuList"]);
            try
            {
                var screen = screens.First(x => x.URLPath != null && URLPath.Contains(x.URLPath));
                HiddenScreenId = screen.NavigationId;
            }
            catch (InvalidOperationException)
            {
                LogWriter.GetLogWriter().Debug("--------------Unable to find NavigateTO entry for----" + URLPath);
            }

            Response.Redirect(URLPath, false);
        }
        public void CallJavaScriptFunction(string functionName)
        {
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(upnlMain, upnlMain.GetType(), "temp", "<script language='javascript' type='text/javascript'>" + functionName + "</script>", false);
        }
        public void CallJavaScriptMessage(string message)
        {
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(upnlMain, upnlMain.GetType(), "temp", "<script language='javascript' type='text/javascript'>alert('" + message + "');</script>", false);
        }
        public override void SetPageTitle(string title)
        {
            lblScreenName.Text = title;
        }
        void UserGroupPopup1_SelectClickHandler(object sender, CommandEventArgs e)
        {
            if (PopupClickHandler != null)
            {
                PopupClickHandler(sender, e);
            }

            UpdateMasterPanel();
            MasterPopup.Hide();
        }
        void SearchUserMapPopup1_SelectClickHandler(object sender, CommandEventArgs e)
        {
            if (PopupClickHandler != null)
            {
                PopupClickHandler(sender, e);
            }
            UpdateMasterPanel();
            MasterPopup.Hide();
        }
        void SearchUserPopup1_SelectClickHandler(object sender, CommandEventArgs e)
        {
            if (PopupClickHandler != null)
            {
                PopupClickHandler(sender, e);
            }

            UpdateMasterPanel();
            MasterPopup.Hide();
        }

        void ReviewersApprovers1_SelectClickHandler(object sender, CommandEventArgs e)
        {
            if (PopupClickHandler != null)
            {
                PopupClickHandler(sender, e);
            }

            UpdateMasterPanel();
            MasterPopup.Hide();
        }

        public override IEnumerable<MenuEntity> GetMenu(int RoleId)
        {
            IList<MenuEntity> screenNameList;

            if (SessionClass.UserMenuList == null)
            {
                screenNameList = new LogonService().GetMenuItems(RoleId);//Now loading from the DB
                SessionClass.UserMenuList = screenNameList;
            }
            else
            {
                screenNameList = SessionClass.UserMenuList;
            }

            return screenNameList;

            //IList<MenuEntity> screenNameList;
            //var cacheObj = _objCacheWriter.GetData("Menu");
            //if (cacheObj == null)
            //{
            //    //screenNameList = new MaintainMenu().GetMenuItems(); //commented to remove Menu.XML loading
            //    screenNameList = new LogonService().GetMenuItems(RoleId);//Now loading from the DB
            //    _objCacheWriter.Add("Menu", screenNameList);
            //}
            //else
            //    screenNameList = (IList<MenuEntity>)cacheObj;
            //screenNameList = screenNameList.ToList();

            //return screenNameList;
        }

        public override void ShowMessage(string message, MessageType messageType)
        {
            if (!string.IsNullOrWhiteSpace(message) && messageType == MessageType.Information)
            {
                AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(upnlMain, upnlMain.GetType(), "temp", "<script language='javascript' type='text/javascript'>toastr.success('" + message + "');</script>", false);
            }
            else if (!string.IsNullOrWhiteSpace(message) && messageType == MessageType.Error)
            {
                AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(upnlMain, upnlMain.GetType(), "temp", "<script language='javascript' type='text/javascript'>toastr.error('" + message + "');</script>", false);
            }

            UpdateMasterPanel();
        }
        public override void HideMasterPopup()
        {
            MasterPopup.Hide();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (OkClickHandler != null)
            {
                OkClickHandler(sender, e);
            }

            UpdateMasterPanel();
            messasgePopup.Hide();
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (YesClickHandler != null)
            {
                YesClickHandler(sender, e);
            }

            UpdateMasterPanel();
            messasgePopup.Hide();
        }

        public override void ResetPage()
        {
            lblMessage.Text = string.Empty;
            UpdateMasterPanel();
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (NoClickHandler != null)
            {
                NoClickHandler(sender, e);
            }

            UpdateMasterPanel();
            messasgePopup.Hide();
        }
        private void UpdateMasterPanel()
        {
            upnlMain.Update();
        }

        public override void ShowPopup(string message)
        {
            lblPopupMessage.Focus();
            lblPopupMessage.Text = message;
            pnlOK.Visible = true;
            pnlYesNo.Visible = false;
            messasgePopup.Show();
        }

        public override void ShowYesNoPopup(string message)
        {
            lblPopupMessage.Focus();
            lblPopupMessage.Text = message;
            pnlOK.Visible = false;
            pnlYesNo.Visible = true;
            messasgePopup.Show();
        }
        public override void RefreshHeader()
        {
            HeaderPanel.Update();
        }
    }
}