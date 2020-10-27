using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class ReportColumnLists : IBusinessEntity
    {
        public virtual int RCID { get; set; }
        public virtual string ColumnName { get; set; }
        public virtual string IsSelected { get; set; }
        public virtual int ScreenId { get; set; }
        public virtual int? ColumnOrder { get; set; }
        public virtual string IsSummary { get; set; }
        public virtual string ViewName { get; set; }
    }
}