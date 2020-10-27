using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class AdminAccessPrivilegeEntity : IBusinessEntity
    {
        public virtual int UserEntityId { get; set; }
        public virtual bool CanSeePassword { get; set; }
    }
}