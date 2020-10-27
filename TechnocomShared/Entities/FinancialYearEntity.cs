using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public partial class FinancialYearEntity : IBusinessEntity
    {
        public int FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}