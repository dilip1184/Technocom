using TechnocomShared.Entities;
using TechnocomService.Audit;
using TechnocomService.SessionManagement;
using TechnocomShared.Enums;
using System;

namespace TechnocomService
{
    public abstract class BaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseService(ContextInfo context)
        {
            SessionContext = context;
            SessionManager.VerifySession(SessionContext.UserId, SessionContext.SessionId);
        }

        protected BaseService()
        {

        }

        /// <summary>
        /// Gets the session context.
        /// </summary>
        protected ContextInfo SessionContext { get; private set; }

        protected void LogActivity(ScreenActivityType screenActivityType, string userComment = "")
        {
            LogActivity(screenActivityType, DateTime.Now, userComment);
        }

        /// <summary>
        /// Logs the activity.
        /// </summary>
        /// <param name="screenActivityType">Type of the screen activity.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="userComment">The user comment.</param>
        protected void LogActivity(ScreenActivityType screenActivityType, DateTime dateTime, string userComment = "")
        {
            AuditLogger.LogActivity(SessionContext.UserId, dateTime, screenActivityType, SessionContext.NavigationId, userComment, SessionContext.BranchId, SessionContext.DeptId);
                                    
        }

        protected void LogActivity(ScreenActivityType screenActivityType, DateTime dateTime, int mapId, int topLevelId, int lowerLevelId, string userComment = "")
        {
            AuditLogger.LogActivity(SessionContext.UserId, dateTime, screenActivityType, SessionContext.NavigationId, userComment, topLevelId, lowerLevelId);
                                   
        }
    }
}