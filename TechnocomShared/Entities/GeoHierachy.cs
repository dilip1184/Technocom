using TechnocomShared.Interfaces;
using System;

namespace TechnocomShared.Entities
{
    [Serializable]
    public partial class RegionEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public long RegionId { get; set; }
        public string RegionName { get; set; }
    }

    [Serializable]
    public partial class ZoneEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public long ZoneId { get; set; }
        public long RegionId { get; set; }
        public string ZoneName { get; set; }

        //
        public string RegionName { get; set; }
    }

    [Serializable]
    public partial class BranchEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public long BranchId { get; set; }
        public long ZoneId { get; set; }
        public long RegionId { get; set; }
        public string BranchName { get; set; }

        //
        public string ZoneName { get; set; }
        public string RegionName { get; set; }
    }

    [Serializable]
    public partial class HubEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public long HubId { get; set; }
        public long BranchId { get; set; }
        public long ZoneId { get; set; }
        public long RegionId { get; set; }
        public string HubName { get; set; }

        //
        public string BranchName { get; set; }
        public string ZoneName { get; set; }
        public string RegionName { get; set; }
    }

    [Serializable]
    public partial class ClusterEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public long ClusterId { get; set; }
        public long HubId { get; set; }
        public long BranchId { get; set; }
        public long ZoneId { get; set; }
        public long RegionId { get; set; }
        public string ClusterName { get; set; }

        //
        public string HubName { get; set; }
        public string BranchName { get; set; }
        public string ZoneName { get; set; }
        public string RegionName { get; set; }
    }

    [Serializable]
    public partial class SiteEntity : IBusinessEntity
    {
        public int OperationId { get; set; }
        public long SiteId { get; set; }
        public long ClusterId { get; set; }
        public long HubId { get; set; }
        public long BranchId { get; set; }
        public long ZoneId { get; set; }
        public long RegionId { get; set; }
        public string ClusterName { get; set; }

        //
        public string SiteName { get; set; }
        public string HubName { get; set; }
        public string BranchName { get; set; }
        public string ZoneName { get; set; }
        public string RegionName { get; set; }
    }
}
