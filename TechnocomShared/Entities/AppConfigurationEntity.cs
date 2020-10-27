using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class AppConfigurationEntity : IBusinessEntity
    {
        public virtual string AppConfCategory { get; set; }
        public virtual string Category { get; set; }
        public virtual string KeyName { get; set; }
        public virtual string KeyValue { get; set; }
        public virtual string KeyValueDataType { get; set; }
        public virtual string KeyDescription { get; set; }
    }
}