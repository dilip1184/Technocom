using System;

using TechnocomShared.Enums;
using TechnocomShared.DataAccess;

namespace TechnocomService.Audit
{
    public static class AuditLogger
    {
        public static void LogActivity(string userId, DateTime auditDateTime, ScreenActivityType screenActivityType,
                                       int screenId, string userComment, int BranchId, int departmentId)
        {
            var parameters = new object[] { userId, auditDateTime, screenActivityType, screenId, userComment, BranchId, departmentId };
            DataConnection.EExecuteNonQuery("InsertLogDetail", parameters);
        }
    }
}