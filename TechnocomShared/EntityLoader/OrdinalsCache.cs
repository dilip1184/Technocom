using System.Collections.Generic;

namespace TechnocomShared.EntityLoader
{
    internal static class OrdinalsCache
    {
        private static readonly Dictionary<string, int[]> Cache =
            new Dictionary<string, int[]>();

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <param name="storedprocedureName">Name of the storedprocedure.</param>
        /// <returns></returns>
        internal static int[] GetCache(string storedprocedureName)
        {
            int[] info = null;
            try
            {
                info = Cache[storedprocedureName];
            }
            catch (KeyNotFoundException)
            {
            }

            return info;
        }

        /// <summary>
        /// Sets the cache.
        /// </summary>
        /// <param name="storedprocedureName">Name of the storedprocedure.</param>
        /// <param name="mappingInfoList">The mapping info list.</param>
        internal static void SetCache(string storedprocedureName, int[] mappingInfoList)
        {
            Cache[storedprocedureName] = mappingInfoList;
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