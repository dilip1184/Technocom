using TechnocomShared.Interfaces;
using System;
using System.Collections.Generic;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class UserSessionData : IBusinessEntity
    {
        public virtual int UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public IList<MenuEntity> MenuList { get; set; }
        public int RoleId { get; set; }
        public string UserDisplayName { get; set; }
    }
}