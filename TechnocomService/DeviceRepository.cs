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
    public class DeviceRepository : BaseService, IBusinessService
    {
        public DeviceRepository()
            : base()
        {
        }
        public DeviceRepository(ContextInfo context)
            : base(context)
        {

        }

        #region DeviceArea
        public IList<DeviceAreaEntity> GetDeviceAreaQuery()
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<DeviceAreaEntity>("SELECT * FROM [DeviceArea]");
            }
            catch (FinderException)
            {
                return new List<DeviceAreaEntity>();
            }
        }
        public IList<DeviceAreaEntity> GetDeviceAreaById(long DeviceAreaId)
        {
            try
            {
                return EntityBase.FillCollectionBySQLQuery<DeviceAreaEntity>("SELECT * FROM [DeviceArea] WHERE [DeviceAreaId]=" + DeviceAreaId + "");
            }
            catch (FinderException)
            {
                return new List<DeviceAreaEntity>();
            }
        }
        public OperationStatusEntity UpdateDeviceArea(DeviceAreaEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.DeviceAreaId, param.DeviceAreaName
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("DeviceAreaManage", parameters);
        }

        #endregion

        #region
        public IList<DeviceEntity> GetAllDeviceQuery(string SN)
        {
            try
            {
                var parameters = new object[] { SN };
                return EntityBase.FillCollection<DeviceEntity>("DeviceListGet", parameters);
            }
            catch (FinderException)
            {
                return new List<DeviceEntity>();
            }
        }
        public OperationStatusEntity UpdateDevice(DeviceEntity param)
        {
            var parameters = new object[] 
                                            { 
                                                param.OperationId, param.DeviceId, param.SN
                                            };
            return EntityBase.FillObject<OperationStatusEntity>("DeviceManage", parameters);
        }

        public string UpdateDeviceStatus(int DeviceId, string DeviceAliasName, bool IsActive)
        {
            try
            {
                DataConnection.ExecuteSQLQuery("UPDATE [Device] SET [DeviceAliasName] ='" + DeviceAliasName + "', [IsActive] = '" + IsActive + "' WHERE [DeviceId] =" + DeviceId + "");
                return "success";
            }
            catch (BaseException be)
            {
                return be.DisplayMessage;
            }
        }

        #endregion
    }
}