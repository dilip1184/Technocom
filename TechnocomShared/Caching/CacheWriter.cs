using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace TechnocomShared.Caching
{
    public class CacheWriter
    {
        private static readonly CacheWriter CacheManager = new CacheWriter();
        private static ICacheManager _cacheManager;

        private CacheWriter()
        {
        }

        public static CacheWriter GetCacheManager()
        {
            _cacheManager = CacheFactory.GetCacheManager("TechnocomCacheManager");
            return CacheManager;
        }

        public void Add(string key, object item)
        {
            _cacheManager.Add(key, item);
            //cacheManager.Add(key, item, null, new SlidingTime(TimeSpan.FromMinutes(5)));
        }

        public object GetData(string key)
        {
            return _cacheManager.GetData(key);
        }

        public bool Contains(string key)
        {
            return _cacheManager.Contains(key);
        }

        public void Remove(string key)
        {
            _cacheManager.Remove(key);
        }

        public void Flush()
        {
            _cacheManager.Flush();
        }
    }
}