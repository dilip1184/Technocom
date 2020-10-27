using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class ContextInfo : IBusinessEntity
    {
        public virtual int UserEntityId { get; set; }
        public virtual string UserId { get; set; }
        public virtual string SessionId { get; set; }
        public virtual int NavigationId { get; set; }
        public virtual int ClassId { get; set; }
        public virtual int BranchId { get; set; }
        public virtual int DeptId { get; set; }
        public virtual int RoleId { get; set; }
    }
}