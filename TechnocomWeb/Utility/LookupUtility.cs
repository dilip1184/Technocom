using iTextSharp.text;
using TechnocomControl;
using TechnocomService;
using TechnocomShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnocomWeb
{
    public class LookupUtility
    {
        public static void BindFinancialYearLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetFinancialYearLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "FinancialYearId";
            ddl.DataTextField = "FinancialYearName";
            ddl.DataBind();
        }
        public static void BindStatusTypeLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetStatusTypeLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "StatusTypeId";
            ddl.DataTextField = "StatusTypeName";
            ddl.DataBind();
        }
        public static void BindUserLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetUserLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "UserId";
            ddl.DataTextField = "UserName";
            ddl.DataBind();
        }
        public static void BindCompanyLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetCompanyLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "CompanyId";
            ddl.DataTextField = "CompanyName";
            ddl.DataBind();
        }
        public static void BindDesignationTypeLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetDesignationTypeLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "DesignationTypeId";
            ddl.DataTextField = "DesignationTypeName";
            ddl.DataBind();
        }
        public static void BindMonthDataLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetMonthDataLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "MonthDataId";
            ddl.DataTextField = "MonthDataName";
            ddl.DataBind();
        }
        public static void BindRegionLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetRegionLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "RegionId";
            ddl.DataTextField = "RegionName";
            ddl.DataBind();
        }
        public static void BindUserRoleLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetUserRoleLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "RoleId";
            ddl.DataTextField = "RoleName";
            ddl.DataBind();
        }
        public static void BindZoneLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetZoneLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "ZoneId";
            ddl.DataTextField = "ZoneName";
            ddl.DataBind();
        }
        public static void BindZoneLookup(ctlDropDownList ddl, ContextInfo SessionContext, long RegionId)
        {
            var list = new SharedRepository(SessionContext).GetZoneLookup().Where(x => x.RegionId == RegionId).ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "ZoneId";
            ddl.DataTextField = "ZoneName";
            ddl.DataBind();
        }
        public static void BindBranchLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetBranchLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "BranchId";
            ddl.DataTextField = "BranchName";
            ddl.DataBind();
        }
        public static void BindBranchLookup(ctlDropDownList ddl, ContextInfo SessionContext, long ZoneId)
        {
            var list = new SharedRepository(SessionContext).GetBranchLookup().Where(x => x.ZoneId == ZoneId).ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "BranchId";
            ddl.DataTextField = "BranchName";
            ddl.DataBind();
        }
        public static void BindHubLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetHubLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "HubId";
            ddl.DataTextField = "HubName";
            ddl.DataBind();
        }
        public static void BindHubLookup(ctlDropDownList ddl, ContextInfo SessionContext, long BranchId)
        {
            var list = new SharedRepository(SessionContext).GetHubLookup().Where(x => x.BranchId == BranchId).ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "HubId";
            ddl.DataTextField = "HubName";
            ddl.DataBind();
        }
        public static void BindClusterLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetClusterLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "ClusterId";
            ddl.DataTextField = "ClusterName";
            ddl.DataBind();
        }
        public static void BindClusterLookup(ctlDropDownList ddl, ContextInfo SessionContext, long HubId)
        {
            var list = new SharedRepository(SessionContext).GetClusterLookup().Where(x => x.HubId == HubId).ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "ClusterId";
            ddl.DataTextField = "ClusterName";
            ddl.DataBind();
        }
        public static void BindSiteLookup(ctlDropDownList ddl, ContextInfo SessionContext)
        {
            var list = new SharedRepository(SessionContext).GetSiteLookup().ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "SiteId";
            ddl.DataTextField = "SiteName";
            ddl.DataBind();
        }
        public static void BindSiteLookup(ctlDropDownList ddl, ContextInfo SessionContext, long ClusterId)
        {
            var list = new SharedRepository(SessionContext).GetSiteLookup().Where(x => x.ClusterId == ClusterId).ToList();

            ddl.DataSource = list;
            ddl.DataValueField = "SiteId";
            ddl.DataTextField = "SiteName";
            ddl.DataBind();
        }
        public static void BindYearLookup(ctlDropDownList ddl)
        {
            Dictionary<int, int> yearList = new Dictionary<int, int>();

            yearList.Add(DateTime.Now.Year, DateTime.Now.Year);
            yearList.Add(DateTime.Now.Year - 1, DateTime.Now.Year - 1);

            ddl.DataSource = yearList;
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataBind();
        }
    }
}