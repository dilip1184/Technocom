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
using TechnocomShared.Helpers;

namespace TechnocomService
{
    public class ConfigrationRepository : BaseService, IBusinessService
    {
        public ConfigrationRepository()
            : base()
        {
        }
        public ConfigrationRepository(ContextInfo context)
            : base(context)
        {

        }

        #region Company
        public IList<CompanyEntity> GetCompanyList()
        {
            try
            {
                var parameters = new object[] { };
                return EntityBase.FillCollection<CompanyEntity>("CompanyList", parameters);
            }
            catch (FinderException)
            {
                return new List<CompanyEntity>();
            }
        }
        public CompanyEntity GetCompanyById(int CompanyId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<CompanyEntity>("SELECT TOP(1) * FROM [Company] WHERE [CompanyId]=" + CompanyId + "").FirstOrDefault();
            }
            catch (FinderException)
            {
                return new CompanyEntity();
            }
        }

        public OperationStatusEntity UpdateCompany(CompanyEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.CompanyId, param.CompanyName, param.CompanyPrefix, param.CIN, param.GSTNumber, param.CompanyAddress1, param.CompanyAddress2, 
                                                param.TownId, param.CompanyContactPerson, param.CompanyMobile, param.CompanyPhone, param.CompanyZipcode, param.CompanyLogo, param.EmailId,
                                                param.Website, param.FinancialYearId, param.IsActive, param.CreatedBy
                                             };

            return EntityBase.FillObject<OperationStatusEntity>("CompanyManage", parameters);
        }

        #endregion

        #region Region
        public IList<RegionEntity> GetAllRegionQuery(string RegionName)
        {
            try
            {
                var parameters = new object[] { RegionName };
                return EntityBase.FillCollection<RegionEntity>("RegionListGet", parameters);
            }
            catch (FinderException)
            {
                return new List<RegionEntity>();
            }
        }
        public IList<RegionEntity> GetRegionById(long RegionId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<RegionEntity>("SELECT * FROM [Region] WHERE [RegionId]=" + RegionId + "");
            }
            catch (FinderException)
            {
                return new List<RegionEntity>();
            }
        }
        public OperationStatusEntity UpdateRegion(RegionEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.RegionId, param.RegionName
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("RegionManage", parameters);
        }

        #endregion

        #region Zone
        public IList<ZoneEntity> GetAllZoneQuery(string ZoneName, long RegionId)
        {
            try
            {
                var parameters = new object[] { ZoneName, RegionId };
                return EntityBase.FillCollection<ZoneEntity>("ZoneListGet", parameters);
            }
            catch (FinderException)
            {
                return new List<ZoneEntity>();
            }
        }
        public IList<ZoneEntity> GetZoneById(long ZoneId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<ZoneEntity>("SELECT * FROM [Zone] WHERE [ZoneId]=" + ZoneId + "");
            }
            catch (FinderException)
            {
                return new List<ZoneEntity>();
            }
        }
        public OperationStatusEntity UpdateZone(ZoneEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.RegionId, param.ZoneId, param.ZoneName
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("ZoneManage", parameters);
        }

        #endregion

        #region Branch
        public IList<BranchEntity> GetAllBranchQuery(string BranchName, long RegionId, long ZoneId)
        {
            try
            {
                var parameters = new object[] { BranchName, RegionId, ZoneId };
                return EntityBase.FillCollection<BranchEntity>("BranchListGet", parameters);
            }
            catch (FinderException)
            {
                return new List<BranchEntity>();
            }
        }
        public IList<BranchEntity> GetBranchById(long BranchId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<BranchEntity>("SELECT * FROM [Branch] WHERE [BranchId]=" + BranchId + "");
            }
            catch (FinderException)
            {
                return new List<BranchEntity>();
            }
        }
        public OperationStatusEntity UpdateBranch(BranchEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.RegionId,  param.ZoneId, param.BranchId, param.BranchName
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("BranchManage", parameters);
        }

        #endregion

        #region Hub
        public IList<HubEntity> GetAllHubQuery(string HubName, long RegionId, long ZoneId, long BranchId)
        {
            try
            {
                var parameters = new object[] { HubName, RegionId, ZoneId, BranchId };
                return EntityBase.FillCollection<HubEntity>("HubListGet", parameters);
            }
            catch (FinderException)
            {
                return new List<HubEntity>();
            }
        }
        public IList<HubEntity> GetHubById(long HubId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<HubEntity>("SELECT * FROM [Hub] WHERE [HubId]=" + HubId + "");
            }
            catch (FinderException)
            {
                return new List<HubEntity>();
            }
        }
        public OperationStatusEntity UpdateHub(HubEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.RegionId,  param.ZoneId,  param.BranchId, param.HubId, param.HubName
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("HubManage", parameters);
        }

        #endregion

        #region Cluster
        public IList<ClusterEntity> GetAllClusterQuery(string ClusterName, long RegionId, long ZoneId, long BranchId, long HubId)
        {
            try
            {
                var parameters = new object[] { ClusterName, RegionId, ZoneId, BranchId, HubId };
                return EntityBase.FillCollection<ClusterEntity>("ClusterListGet", parameters);
            }
            catch (FinderException)
            {
                return new List<ClusterEntity>();
            }
        }
        public IList<ClusterEntity> GetClusterById(long ClusterId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<ClusterEntity>("SELECT * FROM [Cluster] WHERE [ClusterId]=" + ClusterId + "");
            }
            catch (FinderException)
            {
                return new List<ClusterEntity>();
            }
        }
        public OperationStatusEntity UpdateCluster(ClusterEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.RegionId,  param.ZoneId,  param.BranchId, param.HubId, param.ClusterId, param.ClusterName
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("ClusterManage", parameters);
        }

        #endregion

        #region Site
        public IList<SiteEntity> GetAllSiteQuery(string SiteName, long RegionId, long ZoneId, long BranchId, long HubId, long ClusterId)
        {
            try
            {
                var parameters = new object[] { SiteName, RegionId, ZoneId, BranchId, HubId, ClusterId };
                return EntityBase.FillCollection<SiteEntity>("SiteListGet", parameters);
            }
            catch (FinderException)
            {
                return new List<SiteEntity>();
            }
        }
        public IList<SiteEntity> GetSiteById(long SiteId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<SiteEntity>("SELECT * FROM [Site] WHERE [SiteId]=" + SiteId + "");
            }
            catch (FinderException)
            {
                return new List<SiteEntity>();
            }
        }
        public OperationStatusEntity UpdateSite(SiteEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.RegionId,  param.ZoneId,  param.BranchId, param.HubId, param.ClusterId, param.SiteId, param.SiteName
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("SiteManage", parameters);
        }

        #endregion
    }
}