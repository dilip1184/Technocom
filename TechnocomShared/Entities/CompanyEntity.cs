using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{

    [Serializable]
    public partial class CompanyEntity : IBusinessEntity
    {
        public int CreatedBy { get; set; }

        //
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPrefix { get; set; }
        public string CIN { get; set; }
        public string GSTNumber { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public Nullable<long> TownId { get; set; }
        public string CompanyContactPerson { get; set; }
        public string CompanyMobile { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyZipcode { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public int FinancialYearId { get; set; }
        public bool IsActive { get; set; }
        public string CompanyLogo { get; set; }
        public string GSTNumberString { get; set; }
        public string CINString { get; set; }
    }
}