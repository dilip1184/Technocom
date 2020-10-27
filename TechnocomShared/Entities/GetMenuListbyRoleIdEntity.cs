using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{

    [Serializable]
    public class GetMenuListEntity : IBusinessEntity
    {
        public int MenuId { get; set; }
        public Nullable<int> ParentMenuId { get; set; }
        public string MenuName { get; set; }
        public string URLPath { get; set; }
        public string IconClass { get; set; }
        public Nullable<int> SortOrder { get; set; }
    }
}