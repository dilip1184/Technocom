using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    public class FinancialYearLookup : IBusinessEntity
    {
        public int FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class StatusTypeLookup : IBusinessEntity
    {
        public int StatusTypeId { get; set; }
        public string StatusTypeName { get; set; }
    }
    public class AccountLookup : IBusinessEntity
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int VoucherTypeId { get; set; }
    }
    public class UserByDesignationTypeLookup : IBusinessEntity
    {
        public int UserId { get; set; }
        public int? DesignationTypeId { get; set; }
        public string UserName { get; set; }
       
    }

    public class UserLookup : IBusinessEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserNameCode { get; set; }
       
    }
    public class AccountTypeLookup : IBusinessEntity
    {
        public long AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
    }
    public class AssignmentTypeLookup : IBusinessEntity
    {
        public int AssignmentTypeId { get; set; }
        public string AssignmentTypeName { get; set; }
    }
    public class BusinessTypeLookup : IBusinessEntity
    {
        public long BusinessTypeId { get; set; }
        public string BusinessTypeCode { get; set; }
        public string BusinessTypeName { get; set; }
    }

    public class ChannelLookup : IBusinessEntity
    {
        public long ChannelId { get; set; }
        public long ChannelCategoryId { get; set; }
        public Nullable<long> ChannelSubCategoryId { get; set; }
        public string ChannelName { get; set; }
    }

    public class ChannelCategoryLookup : IBusinessEntity
    {
        public long ChannelCategoryId { get; set; }
        public string ChannelCategoryCode { get; set; }
        public string ChannelCategoryName { get; set; }
    }

    public class ChannelSubCategoryLookup : IBusinessEntity
    {
        public long ChannelSubCategoryId { get; set; }
        public long ChannelCategoryId { get; set; }
        public string ChannelSubCategoryCode { get; set; }
        public string ChannelSubCategoryName { get; set; }
    }

    public class ChequeStatusTypeLookup : IBusinessEntity
    {
        public int ChequeStatusTypeId { get; set; }
        public string ChequeStatusTypeName { get; set; }
    }

    public class ChequeTypeLookup : IBusinessEntity
    {
        public int ChequeTypeId { get; set; }
        public string ChequeTypeName { get; set; }
    }

    public class ClassificationLookup : IBusinessEntity
    {
        public long ClassificationId { get; set; }
        public string ClassificationName { get; set; }
    }

    public class CompanyLookup : IBusinessEntity
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
    public class BankAccountLookup : IBusinessEntity
    {
        public int BankAccountId { get; set; }
        public int CompanyId { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
    }
    public class ClaimTypeLookup : IBusinessEntity
    {
        public int ClaimTypeId { get; set; }
        public string ClaimTypeName { get; set; }
    }
    public class DesignationTypeLookup : IBusinessEntity
    {
        public int DesignationTypeId { get; set; }
        public string DesignationTypeName { get; set; }
    }
    public class GSTTypeLookup : IBusinessEntity
    {
        public int GSTTypeId { get; set; }
        public string GSTTypeName { get; set; }
        public string GSTRateType { get; set; }
    }

    public class LocalityLookup : IBusinessEntity
    {
        public long LocalityId { get; set; }
        public string LocalityName { get; set; }
        public long SectionId { get; set; }
        public Nullable<long> LocationId { get; set; }
    }

    public class LocationLookup : IBusinessEntity
    {
        public long LocationId { get; set; }
        public int CompanyId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationNameCode { get; set; }
    }
    public class LocationTypeLookup : IBusinessEntity
    {
        public int LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }
    }

    public class MonthDataLookup : IBusinessEntity
    {
        public int MonthDataId { get; set; }
        public string MonthDataName { get; set; }
    }

    public class NotificationLookup : IBusinessEntity
    {
        public long NotificationId { get; set; }
        public string DocumentName { get; set; }
    }

    public class OutletLookup : IBusinessEntity
    {
        public long OutletId { get; set; }
        public long SectionId { get; set; }
        public long TownId { get; set; }
        public long LocationId { get; set; }
        public string OutletName { get; set; }
    }
    public class PaymentModeLookup : IBusinessEntity
    {
        public int PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
    }

    public class PriceGroupLookup : IBusinessEntity
    {
        public long PriceGroupId { get; set; }
        public string PriceGroupName { get; set; }
    }

    public class PromotionalClassLookup : IBusinessEntity
    {
        public long PromotionalClassId { get; set; }
        public string PromotionalClassCode { get; set; }
        public string PromotionalClassName { get; set; }
    }

    

    public class SectionLookup : IBusinessEntity
    {
        public long SectionId { get; set; }
        public string SectionName { get; set; }
        public long LocationId { get; set; }
        public long TownId { get; set; }
    }

    public class SKUBrandLookup : IBusinessEntity
    {
        public long SKUBrandId { get; set; }
        public long SKUPrincipalId { get; set; }
        public long SKUDivisionId { get; set; }
        public long SKUCategoryId { get; set; }
        public long SKUSubCategoryId { get; set; }
        public string SKUBrandCode { get; set; }
        public string SKUBrandName { get; set; }
    }
    public class SKUVariantLookup : IBusinessEntity
    {
        public long SKUVariantId { get; set; }
        public long SKUSubCategoryId { get; set; }
        public string SKUVariantCode { get; set; }
        public string SKUVariantName { get; set; }
    }
    public class SKUCategoryLookup : IBusinessEntity
    {
        public long SKUCategoryId { get; set; }
        public long SKUPrincipalId { get; set; }
        public long SKUDivisionId { get; set; }
        public string SKUCategoryCode { get; set; }
        public string SKUCategoryName { get; set; }
    }

    public class SKUDivisionLookup : IBusinessEntity
    {
        public long SKUDivisionId { get; set; }
        public long SKUPrincipalId { get; set; }
        public string SKUDivisionName { get; set; }
        public string SKUDivisionCode { get; set; }
    }

    public class SKUGroupLookup : IBusinessEntity
    {
        public long SKUGroupId { get; set; }
        public string SKUGroupName { get; set; }
        public Nullable<long> SKUSubCategoryId { get; set; }
    }

    public class SKUInformationLookup : IBusinessEntity
    {
        public long SKUInformationId { get; set; }
        public string SKUInformationCode { get; set; }
        public string SKUInformationName { get; set; }
        public string SKUInformationNameCode { get; set; }
    }

    public class SKUMasterLookup : IBusinessEntity
    {
        public long SKUMasterId { get; set; }
        public string SKUMasterNameCode { get; set; }
        public string SKUMasterCode { get; set; }
        public string SKUMasterName { get; set; }
        public long SKUBrandId { get; set; }
    }
    public class SKUPrincipalLookup : IBusinessEntity
    {
        public long SKUPrincipalId { get; set; }
        public string SKUPrincipalName { get; set; }
        public string SKUPrincipalCode { get; set; }
        public bool IsChecked { get; set; }
    }
    public class SKUSubCategoryLookup : IBusinessEntity
    {
        public long SKUSubCategoryId { get; set; }
        public long SKUCategoryId { get; set; }
        public long SKUPrincipalId { get; set; }
        public long SKUDivisionId { get; set; }
        public string SKUSubCategoryCode { get; set; }
        public string SKUSubCategoryName { get; set; }
    }
    public class TerritoryLookup : IBusinessEntity
    {
        public long TerritoryId { get; set; }
        public long ZoneId { get; set; }
        public string TerritoryName { get; set; }
    }
    public class TownLookup : IBusinessEntity
    {
        public long TownId { get; set; }
        public long TerritoryId { get; set; }
        public string TownName { get; set; }
    }
    public class LocationByTownLookup : IBusinessEntity
    {
        public long TownId { get; set; }
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationNameCode { get; set; }
    }
    public class UOMLookup : IBusinessEntity
    {
        public int UOMId { get; set; }
        public string UOMName { get; set; }
    }

    public class UserRoleLookup : IBusinessEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class VisitFrequencyLookup : IBusinessEntity
    {
        public long VisitFrequencyId { get; set; }
        public string VisitFrequencyName { get; set; }
    }
    public class VoucherTypeLookup : IBusinessEntity
    {
        public int VoucherTypeId { get; set; }
        public string VoucherTypeName { get; set; }
    }
    public class VoucherSubTypeLookup : IBusinessEntity
    {
        public int VoucherSubTypeId { get; set; }
        public int VoucherTypeId { get; set; }
        public string VoucherSubTypeName { get; set; }
    }
    public class RegionLookup : IBusinessEntity
    {
        public long RegionId { get; set; }
        public string RegionName { get; set; }
    }
    public class ZoneLookup : IBusinessEntity
    {
        public long ZoneId { get; set; }
        public long RegionId { get; set; }
        public string ZoneName { get; set; }
    }
    public class BranchLookup : IBusinessEntity
    {
        public long BranchId { get; set; }
        public long RegionId { get; set; }
        public long ZoneId { get; set; }
        public string BranchName { get; set; }
    }
    public class HubLookup : IBusinessEntity
    {
        public long HubId { get; set; }
        public long RegionId { get; set; }
        public long ZoneId { get; set; }
        public long BranchId { get; set; }
        public string HubName { get; set; }
    }
    public class ClusterLookup : IBusinessEntity
    {
        public long ClusterId { get; set; }
        public long RegionId { get; set; }
        public long ZoneId { get; set; }
        public long BranchId { get; set; }
        public long HubId { get; set; }
        public string ClusterName { get; set; }
    }
    public class SiteLookup : IBusinessEntity
    {
        public long SiteId { get; set; }
        public long RegionId { get; set; }
        public long ZoneId { get; set; }
        public long BranchId { get; set; }
        public long HubId { get; set; }
        public long ClusterId { get; set; }
        public string SiteName { get; set; }
    }
    public class AccountCategoryLookup : IBusinessEntity
    {
        public long AccountCategoryId { get; set; }
        public string AccountCategoryName { get; set; }
    }

    public class COAMainTypeLookup : IBusinessEntity
    {
        public long COAMainTypeId { get; set; }
        public string COAMainTypeName { get; set; }
    }
    public class COASubTypeLookup : IBusinessEntity
    {
        public long COAMainTypeId { get; set; }
        public long COASubTypeId { get; set; }
        public string COASubTypeName { get; set; }
    }
    public class COADetailTypeLookup : IBusinessEntity
    {
        public long COASubTypeId { get; set; }
        public long COADetailTypeId { get; set; }
        public string COADetailTypeName { get; set; }
    }
    public class COAAccountHeadLookup : IBusinessEntity
    {
        public long COAAccountHeadId { get; set; }
        public string COAAccountHeadName { get; set; }
    }
    public class COAAccountHeadByVaucherTypeLookup : IBusinessEntity
    {
        public long COAAccountHeadId { get; set; }
        public long VaucherType { get; set; }
        public string COAAccountHeadName { get; set; }
    }

    public class ROIInputHeadLookup : IBusinessEntity
    {
        public long ROIInputHeadId { get; set; }
        public string ROIInputHeadName { get; set; }
    }
}