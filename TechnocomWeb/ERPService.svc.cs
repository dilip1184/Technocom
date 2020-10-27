using TechnocomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace TechnocomWeb
{
    [ServiceBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class ERPService : IERPService
    {
        string IERPService.SetAttendance(string applicationId, string userSystemCode, string year, string month, string day, string hour, string minute, string second, string MachineId, string attLogStamp)
        {
            try
            {
                return new AttendanceService().InsertAttendanceData(userSystemCode, year, month, day, hour, minute, second, MachineId, attLogStamp);

            }
            catch { }

            return "OK";
        }
        string IERPService.GetLastAttLogStamp(string applicationId, string MachineId)
        {
            try
            {
                return new AttendanceService().GetLastAttLogStamp(MachineId);
            }
            catch { }

            return "OK";
        }
        string IERPService.GetMachineCommand(string applicationId, string MachineId)
        {
            try
            {
                return new AttendanceService().GetMachineCommand(MachineId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        string IERPService.SetMachineCommandResponse(string applicationId, string MachineId, string commandId, string responseCode)
        {
            try
            {
                return new AttendanceService().SetMachineCommandResponse(MachineId, commandId, responseCode);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        void IERPService.SetDummyAttendance(string MachineId, string logDateTime)
        {
            try
            {
                new AttendanceService().InsertDummyAttendanceData(MachineId, logDateTime);
            }
            catch { }
        }
        string IERPService.Test(string id)
        {
            return "OK";
        }
    }
}
