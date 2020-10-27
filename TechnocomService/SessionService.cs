using TechnocomShared.DataAccess;
using TechnocomShared.Entities;
using TechnocomShared.EntityLoader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnocomService
{
    public class SessionService
    {
        public UserSession CreateSession(string LoginId, string sessionid, string userIp, DateTime lastActivity)
        {
            var parameters = new object[] { LoginId, sessionid, userIp, lastActivity };
            return EntityBase.FillObject<UserSession>("SessionCreate", parameters);
        }
        public void UpdateSession(string LoginId, string SessionId, DateTime lastActivity)
        {
            DataConnection.ExecuteSQLQuery("UPDATE UserSession SET LastActivity ='" + lastActivity + "' WHERE UserID ='" + LoginId + "' AND SessionId ='" + SessionId + "' ");
        }
        public void MarkSessionInactive(string LoginId)
        {
            DataConnection.ExecuteSQLQuery("UPDATE UserSession SET Active=0 WHERE UserID ='" + LoginId + "' ");
        }
        public void DeleteSession(string LoginId, string SessionId)
        {
            DataConnection.ExecuteSQLQuery("DELETE FROM UserSession WHERE UserID ='" + LoginId + "' AND SessionId ='" + SessionId + "' ");
        }
        public IEnumerable<UserSession> DeleteSessionAndLockedData(int sessionClearingTime)
        {
            var parameters = new object[] { sessionClearingTime };
            return EntityBase.FillCollection<UserSession>("SessionDeleteAndLockedData", parameters);
        }
        public UserSession ValidateSession(string LoginId, string Sessionid)
        {
            return EntityBase.FillCollectionBySQLQuery<UserSession>("SELECT * FROM UserSession WHERE Active = 1 AND UserID = '" + LoginId + "' AND Sessionid = '" + Sessionid + "'").FirstOrDefault();
        }
    }
}
