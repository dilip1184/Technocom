using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class UserSession : IBusinessEntity
    {
        public virtual string UserId { get; set; }
        public virtual string Sessionid { get; set; }
        public virtual string User_IP { get; set; }
        public virtual DateTime LastActivity { get; set; }
    }
}