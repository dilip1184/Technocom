using System;
using TechnocomShared.Interfaces;

namespace TechnocomShared.Entities
{
    [Serializable]
    public class Pager : IBusinessEntity
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public string ShortExpression { get; set; }
    }
}
