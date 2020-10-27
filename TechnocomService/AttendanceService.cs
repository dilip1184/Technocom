using TechnocomShared.DataAccess;
using TechnocomShared.Entities;
using TechnocomShared.EntityLoader;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;
using TechnocomShared.Helpers;
using TechnocomShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnocomService
{
    public class AttendanceService : BaseService, IBusinessService
    {
        public AttendanceService(ContextInfo context)
            : base(context)
        {
        }

        public AttendanceService()
            : base()
        {
        }

        public string InsertAttendanceData(string BiometricCode, string year, string month, string day, string hour, string minute, string second, string MachineId, string attLogStamp)
        {
            DateTime date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day),
                Convert.ToInt32(hour), Convert.ToInt32(minute), Convert.ToInt32(second));

            var parameters = new object[] { BiometricCode, date.Date, date.TimeOfDay, MachineId, attLogStamp };
            DataConnection.EExecuteNonQuery("InsertAttendanceData", parameters);
            return "OK";
        }
        public void ServiceMarkAttendance(string EntityIds, string ExecutedBy)
        {
            try
            {
                var parameters = new object[] { EntityIds, ExecutedBy };
                DataConnection.EExecuteNonQuery("ServiceMarkAttendance", parameters);
            }
            catch { }
        }
        public string GetLastAttLogStamp(string MachineId)
        {
            try
            {
                var parameters = new object[] { MachineId };
                return DataConnection.EExecuteScalar("GetLastAttLogStamp", parameters).ToString();
            }
            catch { return string.Empty; }
        }

        public string GetMachineCommand(string MachineId)
        {
            try
            {
                var parameters = new object[] { MachineId };
                string command = DataConnection.EExecuteScalar("GetDeviceCommand", parameters).ToString();
                return command;
            }
            catch { return string.Empty; }
        }

        public string SetMachineCommandResponse(string MachineId, string commandId, string responseCode)
        {
            try
            {
                var parameters = new object[] { MachineId, commandId, responseCode };
                string command = DataConnection.EExecuteScalar("SetMachineCommandResponse", parameters).ToString();
                return command;
            }
            catch { return string.Empty; }
        }

        public void SaveAlertDeliveryReport(string status, string alertMessageId)
        {
            var parameters = new object[] { DateTime.Now, status, alertMessageId };
            DataConnection.EExecuteNonQuery("SaveAlertDeliveryReport", parameters);
        }

        public void InsertDummyAttendanceData(string MachineId, string logDateTime)
        {
            try
            {
                string formatString = "yyyyMMddHHmmss";
                DateTime date = DateTime.ParseExact(logDateTime, formatString, null);
                var parameters = new object[] { MachineId, date };
                DataConnection.EExecuteNonQuery("InsertDummyAttendanceData", parameters);
            }
            catch { }
        }
        public void CreateManualAttendanceCalendar(long UserEntityId, int AlertCategoryId, string PunchDate, string PunchTime, string CreatedBy)
        {
            var parameters = new object[] { UserEntityId, AlertCategoryId, PunchDate, PunchTime, CreatedBy };
            DataConnection.EExecuteNonQuery("AttendanceTempDataProcess", parameters);
        }
    }
}
