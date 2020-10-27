using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{

    [Serializable]
    public partial class DeviceEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public int DeviceId { get; set; }
        public string SN { get; set; }
        public string DeviceAliasName { get; set; }
        public string IPAddress { get; set; }
        public string ServerPort { get; set; }
        public string DeviceModel { get; set; }
        public string AttLogStamp { get; set; }
        public string OPLogStamp { get; set; }
        public string AttPhotoStamp { get; set; }
        public string GPSLocation { get; set; }
        public int DeviceAreaId { get; set; }
        public bool IsActive { get; set; }
        public string MachineStatus { get; set; }
        public string LogDateTime { get; set; }
    }
}