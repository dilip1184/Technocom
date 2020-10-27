using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class MenuEntity : IBusinessEntity
    {
        public virtual int NavigationId { get; set; }
        public virtual string MenuName { get; set; }
        public virtual int ParentId { get; set; }
        public virtual string URLPath { get; set; }
        public virtual string IconClass { get; set; }
        public virtual int SortOrder { get; set; }
    }
} 