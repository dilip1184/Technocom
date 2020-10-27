using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
     [Serializable]
    public class UserEntity : IBusinessEntity
    {
        public int UserId { get; set; }
        public string UserEmailId { get; set; }
        public string LoginId { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string EnrollmentNo { get; set; }
        public string AadharNo { get; set; }
        public string MobileNumber { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string PinCode { get; set; }
        public string PhotoPath { get; set; }
        public int DesignationTypeId { get; set; }
        public string CTCYearly { get; set; }
        public string SalaryMonthly { get; set; }
        public int ShiftId { get; set; }
        public string WorkingHour { get; set; }
        public string UserLastPasswordChangeString { get; set; }
        public string UserLastLoginString { get; set; }
        public bool CanOperateUser { get; set; }
        public bool CanUploadPOD { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDateString { get; set; }
        public string ModifiedDateString { get; set; }
        public string ShiftName { get; set; }
        public string RoleName { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CreatedByUser { get; set; }
        public string ModifiedByUser { get; set; }
    }
}