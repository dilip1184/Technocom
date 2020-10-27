using System;
using System.Collections.Generic;
using TechnocomShared.Entities;
using TechnocomShared.Configuration;
using TechnocomShared.Constants;
using TechnocomShared.Exceptions;

namespace TechnocomService.SessionManagement
{
    public sealed class SessionManager
    {
        private static readonly int SessionTimeout = AppConfigurationHelper.GetValue<int>(ConfigKeys.SessionTimeout);

        /// <summary>
        /// Verifies the session.
        /// </summary>
        /// <param name="LoginId">Name of the user.</param>
        /// <param name="sessionId">The session id.</param>
        public static void VerifySession(string LoginId, string sessionId)
        {
            try
            {
                var userSession = GetSession().ValidateSession(LoginId, sessionId);
                if (userSession.LastActivity.AddMinutes(SessionTimeout) < DateTime.Now)
                    throw new SessionTimeoutException();

                UpdateSession(LoginId, sessionId);
            }
            catch (FinderException)
            {
                throw new SessionTimeoutException();
            }
            catch (SessionTimeoutException)
            {
                LogOff(LoginId, sessionId);
                throw;
            }
        }

        /// <summary>
        /// Updates the session.
        /// </summary>
        /// <param name="LoginId">Name of the user.</param>
        /// <param name="sessionId">The session id.</param>
        private static void UpdateSession(string LoginId, string sessionId)
        {
            GetSession().UpdateSession(LoginId, sessionId, DateTime.Now);
        }

        /// <summary>
        /// Creates the session.
        /// </summary>
        /// <param name="LoginId">Name of the user.</param>
        /// <param name="UserIP">The user ip.</param>
        /// <returns></returns>
        public static UserSession CreateSession(string LoginId, string UserIP)
        {
            InvalidateSession(LoginId);
            return GetSession().CreateSession(LoginId, Guid.NewGuid().ToString(), UserIP, DateTime.Now);
        }

        /// <summary>
        /// Invalidates the session.
        /// </summary>
        /// <param name="LoginId">Name of the user.</param>
        private static void InvalidateSession(string LoginId)
        {
            try
            {
                GetSession().MarkSessionInactive(LoginId);
            }
            catch (FinderException) //consume exception
            {
            }
          }

        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <param name="LoginId">Name of the user.</param>
        /// <param name="sessionId">The session id.</param>
        public static void LogOff(string LoginId, string sessionId)
        {
            try
            {
                GetSession().DeleteSession(LoginId, sessionId);
               
                
            }
            catch (BaseException) //consume exception
            {
            }
            
        }

        /// <summary>
        /// deletes the session and clears the locked data 
        /// </summary>
        /// <param name="sessionClearingTime"></param>
        public static IEnumerable<UserSession> DeleteSessionAndLockedData(int sessionClearingTime)
        {
            try
            {
                return GetSession().DeleteSessionAndLockedData(sessionClearingTime);
            }
            catch (BaseException) //consume exception
            {
                return null;
            }
        }

        /// <summary>
        /// Checks the inactive sessions.
        /// </summary>
        public void CheckInactiveSessions()
        {
            //invoked via the bg thread
            // Check inactive sessions based on inacitivity timeout,
            // call logoff
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns></returns>
        private static SessionService GetSession()
        {
            return new SessionService();
        }
    }
}