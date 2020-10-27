using TechnocomShared.Entities;
using TechnocomShared.Constants;
using TechnocomShared.DataAccess;
using TechnocomShared.EntityLoader;
using TechnocomShared.Enums;
using TechnocomShared.Exceptions;
using TechnocomShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnocomService
{
    public class SharedRepository : BaseService, IBusinessService
    {
        public SharedRepository()
            : base()
        {
        }
        public SharedRepository(ContextInfo context)
            : base(context)
        {

        }

        public IList<FinancialYearLookup> GetFinancialYearLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<FinancialYearLookup>("SELECT * FROM [FinancialYear] WHERE IsActive = 'True'");
            }
            catch (FinderException)
            {
                return new List<FinancialYearLookup>();
            }
        }

        public IList<StatusTypeLookup> GetStatusTypeLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<StatusTypeLookup>("SELECT * FROM [StatusType]");
            }
            catch (FinderException)
            {
                return new List<StatusTypeLookup>();
            }
        }
        public IList<UserLookup> GetUserLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<UserLookup>("SELECT * FROM [Users] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<UserLookup>();
            }
        }
        public IList<CompanyLookup> GetCompanyLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<CompanyLookup>("SELECT * FROM [Company] WHERE IsDeleted = 'false' AND IsActive = 'true'");
            }
            catch (FinderException)
            {
                return new List<CompanyLookup>();
            }
        }
        public IList<DesignationTypeLookup> GetDesignationTypeLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<DesignationTypeLookup>("SELECT * FROM [DesignationType] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<DesignationTypeLookup>();
            }
        }
        public IList<MonthDataLookup> GetMonthDataLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<MonthDataLookup>("SELECT * FROM [MonthData]");
            }
            catch (FinderException)
            {
                return new List<MonthDataLookup>();
            }
        }
        public IList<RegionLookup> GetRegionLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<RegionLookup>("SELECT * FROM [Region] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<RegionLookup>();
            }
        }
        public IList<UserRoleLookup> GetUserRoleLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<UserRoleLookup>("SELECT * FROM [Role] WHERE RoleId != 1");
            }
            catch (FinderException)
            {
                return new List<UserRoleLookup>();
            }
        }
        public IList<ZoneLookup> GetZoneLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<ZoneLookup>("SELECT * FROM [Zone] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<ZoneLookup>();
            }
        }
        public IList<BranchLookup> GetBranchLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<BranchLookup>("SELECT * FROM [Branch] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<BranchLookup>();
            }
        }
        public IList<HubLookup> GetHubLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<HubLookup>("SELECT * FROM [Hub] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<HubLookup>();
            }
        }
        public IList<ClusterLookup> GetClusterLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<ClusterLookup>("SELECT * FROM [Cluster] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<ClusterLookup>();
            }
        }
        public IList<SiteLookup> GetSiteLookup()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<SiteLookup>("SELECT * FROM [Site] WHERE IsDeleted = 'false'");
            }
            catch (FinderException)
            {
                return new List<SiteLookup>();
            }
        }
    }
}