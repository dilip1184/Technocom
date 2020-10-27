using System.Collections.Generic;

namespace TechnocomShared.EntityLoader
{
    internal static class MappingInfoCache
    {
        private static readonly Dictionary<string, IList<PropertyMappingInfo>> Cache =
            new Dictionary<string, IList<PropertyMappingInfo>>();

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        internal static IList<PropertyMappingInfo> GetCache(string typeName)
        {
            List<PropertyMappingInfo> info = null;
            try
            {
                info = (List<PropertyMappingInfo>) Cache[typeName];
            }
            catch (KeyNotFoundException)
            {
            }

            return info;
        }

        /// <summary>
        /// Sets the cache.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="mappingInfoList">The mapping info list.</param>
        internal static void SetCache(string typeName, IList<PropertyMappingInfo> mappingInfoList)
        {
            Cache[typeName] = mappingInfoList;
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public static void ClearCache()
        {
            Cache.Clear();
        }
    }
}