using TechnocomShared.Collection;
using TechnocomShared.Entities;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;
using TechnocomShared.Helpers;
using TechnocomShared.Interfaces;
using TechnocomShared.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnocomControl
{
    public class ctlPage : Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            //ClientTarget = "uplevel";

            //if (!Request.IsLocal && !Request.IsSecureConnection)
            //{
            //    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
            //    Response.Redirect(redirectUrl);
            //}
        }
        protected override object LoadPageStateFromPersistenceMedium()
        {
            try
            {
                var compression = ConfigurationManager.AppSettings["ViewStateCompression"];
                if (string.IsNullOrEmpty(compression)) compression = "true";
                if (bool.Parse(compression))
                {
                    var viewState = Request.Form["__VSTATE"];
                    if (viewState.EndsWith(",")) viewState = viewState.Substring(0, viewState.Length - 1);
                    var bytes = Convert.FromBase64String(viewState);
                    bytes = Compressor.Decompress(bytes);

                    viewState = Convert.ToBase64String(bytes);
                    if (string.IsNullOrEmpty(viewState)) return null;
                    var formatter = new LosFormatter();
                    return formatter.Deserialize(viewState);
                }
                else
                    return base.LoadPageStateFromPersistenceMedium();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Saves the ViewState from the persistence medium
        /// </summary>
        protected override void SavePageStateToPersistenceMedium(object state)
        {
            var writer = new StringWriter();
            try
            {
                var compression = ConfigurationManager.AppSettings["ViewStateCompression"];
                if (string.IsNullOrEmpty(compression)) compression = "true";
                if (bool.Parse(compression))
                {
                    var formatter = new LosFormatter();
                    formatter.Serialize(writer, state);
                    var vState = writer.ToString();
                    var bytes = Convert.FromBase64String(vState);
                    bytes = Compressor.Compress(bytes);
                    vState = Convert.ToBase64String(bytes);

                    var sm = ScriptManager.GetCurrent(this);
                    if (sm != null && sm.IsInAsyncPostBack)
                        ScriptManager.RegisterHiddenField(this, "__VSTATE", vState);
                    else
                        Page.ClientScript.RegisterHiddenField("__VSTATE", vState);
                }
                else
                    base.SavePageStateToPersistenceMedium(state);
            }
            catch (Exception) { throw; }
            finally
            {
                if (writer != null) { writer.Dispose(); writer = null; }
            }
        }

        /// <summary>
        /// Occurs when [data load handler].
        /// </summary>
        public event OnDataLoad DataLoadHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        public delegate void OnDataLoad(ctlPage sender, CommandEventArgs e);

        /// <summary>
        /// Occurs when [validation handler].
        /// </summary>
        public event OnValidate ValidationHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        public delegate void OnValidate(ctlPage sender, CommandEventArgs e);


        /// <summary>
        /// Resets the controls.
        /// </summary>
        public void ResetControls()
        {
            ValidationHandler(this, new CommandEventArgs("Reset", ""));
        }


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (UserData == null)
                throw new SessionTimeoutException("Session State Expired.");

            if (!IsPostBack)
            {
                AsyncMode = true;
                var screens = MasterPage.GetMenu(UserData.RoleId);
                var currentPath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath +
                                  HttpContext.Current.Request.Url.Query + ".aspx";
                try
                {
                    var screen = screens.First(x => x.URLPath != null && currentPath == x.URLPath);
                    MasterPage.SetPageTitle(screen.MenuName);
                    MasterPage.NavigationId = screen.NavigationId;

                    if (MasterPage.HiddenScreenId != MasterPage.NavigationId)
                    {
                        //  throw new SessionTimeoutException("Screen Ids do not match");
                    }

                    //if ((screen.Visible) && (!HasPermission(MasterPage.ScreenId)))
                    //    throw new AuthorizationException("User not allowed to access " + MasterPage.ScreenId);
                }
                catch (InvalidOperationException)
                {
                    LogWriter.GetLogWriter().Debug("--------------Unable to find menu entry for----" + currentPath);
                }
            }

            base.OnInit(e);
        }

        /// <summary>
        /// When implemented, gets a collection of information that can be accessed by a control designer.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IDictionary"/> containing information about the control.</returns>
        internal UserSessionData UserData
        {
            get { return MasterPage.UserData; }
        }

        public ContextInfo SessionContext
        {
            get { return MasterPage.SessionContext; }
        }

        public long UserId
        {
            get { return UserData.UserId; }
        }

        public UserEntity UserEntity
        {
            get
            {
                return (UserEntity)Session["UserEntity"];
            }
        }
        private void ShowMessage(string message, MessageType messageType)
        {
            if (MasterPage != null)
            {
                MasterPage.ShowMessage(message, messageType);
            }
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowErrorMessage(string message)
        {
            ShowMessage(message, MessageType.Error);
        }

        /// <summary>
        /// Shows the info message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowInfoMessage(string message)
        {
            ShowMessage(message, MessageType.Information);
        }

        /// <summary>
        /// Shows the popup.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void ShowPopup(string message)
        {
            if (MasterPage != null)
                MasterPage.ShowPopup(message);
        }

        /// <summary>
        /// Shows the yes no popup.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowYesNoPopup(string message)
        {
            if (MasterPage != null)
                MasterPage.ShowYesNoPopup(message);
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        public ctlMasterPage MasterPage
        {
            get
            {
                if (Master != null)
                    return (ctlMasterPage)Master;
                return null;
            }
        }


        /// <summary>
        /// Navigates to specified path
        /// </summary>
        /// <param name="path">The path.</param>
        public void NavigateTo(string path)
        {
            var screens = MasterPage.GetMenu(UserData.RoleId);
            try
            {
                var screen = screens.First(x => x.URLPath != null && path.Contains(x.URLPath));
                MasterPage.HiddenScreenId = screen.NavigationId;
            }
            catch (InvalidOperationException)
            {
                LogWriter.GetLogWriter().Debug("--------------Unable to find NavigateTO entry for----" + path);
            }

            Response.Redirect(path, false);
        }
        //protected void NavigateToReport(GenericCollection<IList<IBusinessEntity>> data, ExportFormat format, string[] reportFields)
        //{
        //    var reportHeaders = new Dictionary<string, string>();
        //    NavigateToReport(data, format, reportFields, reportHeaders);
        //}

        //protected void NavigateToReport(GenericCollection<IList<IBusinessEntity>> data, ExportFormat format, string[] reportFields, Dictionary<string, string> reportHeaders)
        //{
        //    Session["ExportData"] = data;
        //    Session["ReportHeaders"] = reportHeaders;

        //    if (reportFields != null)
        //        Session["ReportFields"] = reportFields;

        //    Response.Redirect("~/UI/Download.aspx?f=" + (int)format, false);
        //}
        public void NavigateToHome()
        {
            NavigateTo("~/UI/DashBoard.aspx");
        }
        public static object GetObjectFromObjectCollection(IList<object> objList, string metaSourceName)
        {
            object response = null;
            if (metaSourceName != null)
                response = objList.FirstOrDefault(obj => obj.GetType().FullName.Contains(metaSourceName));
            return response;
        }
        protected void BindDataToDifferentPageControls(IList<object> objList)
        {
            if (DataLoadHandler != null)
                DataLoadHandler(this, new CommandEventArgs("Load", objList));
        }

        protected void ValidateBusinessData(string strGroupName)
        {
            if (ValidationHandler != null)
            {
                var groupNames = strGroupName.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in groupNames)
                {
                    ValidationHandler(this, new CommandEventArgs("Validate", t));
                }
            }

            if (_sbMessages.ToString() != string.Empty)
                throw new ValidationException(_sbMessages.ToString());

            ShowInfoMessage("");
        }

        readonly StringBuilder _sbMessages = new StringBuilder();

        public string ErrorMessage
        {
            set
            {
                if (!_sbMessages.ToString().Contains(value))
                    _sbMessages.Append(value);
            }
        }
    }
}
