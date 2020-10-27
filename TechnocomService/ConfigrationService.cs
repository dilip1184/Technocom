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
    public class ConfigrationService : BaseService, IBusinessService
    {
        public ConfigrationService()
            : base()
        {
        }
        public ConfigrationService(ContextInfo context) : base(context)
        {

        }

        public IList<AppConfigurationEntity> GetAll()
        {
            return EntityBase.FillCollectionBySQLQuery<AppConfigurationEntity>("SELECT * FROM AppConfiguration");
        }

        // Don't Delete : To be called from AppConfigurationHelper
        public IDictionary<string, string> GetAllConfigurations()
        {
            var appConfigurationList = GetAll();
            return appConfigurationList.ToDictionary(appConfigurationEntity => appConfigurationEntity.KeyName,
                                                     appConfigurationEntity => appConfigurationEntity.KeyValue,
                                                     StringComparer.OrdinalIgnoreCase);
        }
    }
}