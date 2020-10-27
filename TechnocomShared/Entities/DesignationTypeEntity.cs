using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{

    [Serializable]
    public partial class DesignationTypeEntity : IBusinessEntity
    {
        public long DesignationTypeId { get; set; }
        public string DesignationTypeName { get; set; }
        public bool IsChecked { get; set; }
    }
}