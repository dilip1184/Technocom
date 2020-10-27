using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TechnocomWeb
{
    [ServiceContract(Namespace = "http://fricco.co.in")]
    public interface IERPService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/SetAttendance?applicationId={applicationId}&userSystemCode={userSystemCode}&year={year}&month={month}&day={day}&hour={hour}&minute={minute}&second={second}&MachineId={MachineId}&attLogStamp={attLogStamp}")]
        string SetAttendance(string applicationId, string userSystemCode, string year, string month, string day, string hour, string minute, string second, string MachineId, string attLogStamp);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/GetLastAttLogStamp?applicationId={applicationId}&MachineId={MachineId}")]
        string GetLastAttLogStamp(string applicationId, string MachineId);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/GetMachineCommand?applicationId={applicationId}&MachineId={MachineId}")]
        string GetMachineCommand(string applicationId, string MachineId);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/SetMachineCommandResponse?applicationId={applicationId}&MachineId={MachineId}&commandId={commandId}&responseCode={responseCode}")]
        string SetMachineCommandResponse(string applicationId, string MachineId, string commandId, string responseCode);


        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "/SetDummyAttendance?MachineId={MachineId}&logDateTime={logDateTime}")]
        void SetDummyAttendance(string MachineId, string logDateTime);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "Test/{id}")]
        string Test(string id);
    }

    [ServiceContract(Namespace = "http://test.fricco.co.in")]
    public interface IExternalService : IERPService
    {

    };
}
