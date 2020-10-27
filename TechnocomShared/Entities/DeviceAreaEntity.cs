using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{

    [Serializable]
    public partial class DeviceAreaEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public int DeviceAreaId { get; set; }
        public string DeviceAreaName { get; set; }
    }
}