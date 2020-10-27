using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{

    [Serializable]
    public class RoleManagementEntity : IBusinessEntity
    {
        public int OperationId { get; set; }

        //
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    [Serializable]
    public class MenuItemEntity : IBusinessEntity
    {
        public int RoleId { get; set; }
        
        //
        public int NavigationId { get; set; }
        public int ParentId { get; set; }
        public string MenuName { get; set; }
        public string URLPath { get; set; }
        public string IconClass { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public bool IsChecked { get; set; }
    }

    [Serializable]
    public partial class UserRoleEntity : IBusinessEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}