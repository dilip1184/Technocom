using TechnocomShared.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TechnocomShared.Configuration
{
    public static class AppConfigurationHelper
    {
        private static readonly IDictionary<string, string> Configurations = LoadConfiguration();

        private static IDictionary<string, string> LoadConfiguration()
        {
            var assembly = Assembly.Load("TechnocomService");
            var ab = assembly.CreateInstance("TechnocomService.ConfigrationService");
            var obj = ab.GetType().InvokeMember(null,
                                                   BindingFlags.Public | BindingFlags.Instance |
                                                   BindingFlags.CreateInstance, null, null, null);
            return
                (IDictionary<string, string>)
                ab.GetType().InvokeMember("GetAllConfigurations", BindingFlags.InvokeMethod, null, obj, null);
        }

        public static T GetValue<T>(string keyName)
        {
            try
            {
                var type = typeof(T);
                return (T)(type.BaseType.Equals(typeof(Enum))
                                ? Enum.ToObject(type, int.Parse(Configurations[keyName]))
                                : Convert.ChangeType(Configurations[keyName], typeof(T)));
            }
            catch (Exception ex)
            {
                throw new TechnicalException("Error occured while retrieving key:" + keyName ,ex);
            }
        }
    }
}