using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnocomShared.Entities;
using TechnocomShared.Enums;

namespace TechnocomControl
{
    public abstract class ctlMasterPage : MasterPage
    {
        public virtual event OnOkButtonClick OkClickHandler;
        public delegate void OnOkButtonClick(object sender, EventArgs e);

        public virtual event OnYesButtonClick YesClickHandler;
        public delegate void OnYesButtonClick(object sender, EventArgs e);

        public virtual event OnNoButtonClick NoClickHandler;
        public delegate void OnNoButtonClick(object sender, EventArgs e);

        public virtual event OnPopupButtonClick PopupClickHandler;
        public delegate void OnPopupButtonClick(object sender, CommandEventArgs e);

        public const string UserSessionObject = "UserData";
        public const string UserEntity = "UserEntity";
        public const string ContextInfo = "ContextInfo";
        public abstract void SetPageTitle(string title);
        public abstract void ResetPage();

        public int HiddenScreenId
        {
            get { return Convert.ToInt32(Session["Scr"] ?? 1); }
            set
            {
                Session["Scr"] = value;
            }
        }
        public abstract void ShowMessage(string message, MessageType messageType);

        public UserSessionData UserData
        {
            get
            {
                return (UserSessionData)Session[UserSessionObject];
            }
            set { Session[UserSessionObject] = value; }
        }

        public ContextInfo SessionContext
        {
            get
            {
                return (ContextInfo)Session[ContextInfo];
            }
        }
        public int NavigationId
        {
            get { return SessionContext.NavigationId; }
            set { SessionContext.NavigationId = value; }
        }

        public abstract IEnumerable<MenuEntity> GetMenu(int RoleId);
        public abstract void RefreshHeader();
        public abstract void ShowPopup(string message);
        public abstract void ShowYesNoPopup(string message);
        public abstract void HideMasterPopup();
    }
}
