using System;
using System.Runtime.Caching;
using Infrastructure.Configuration;

namespace Infrastructure.Cache
{
    public class DesktopApplicationCacheProvider : IApplicationCacheProvider
    {
        private static readonly ObjectCache Cache = MemoryCache.Default; 

        public void Put(string key, object data)
        {
            Cache.Add(key, data, DateTime.Now.AddMinutes(Configurations.CacheExpirationInMinutes));
        }

        public void Put(string key, object data, int timespanInMinutes)
        {
            Cache.Add(key, data, DateTime.Now.AddMinutes(timespanInMinutes));
        }

        public T Get<T>(string key)
        {
            if (Cache[key] == null)
            {
                return default(T);
            }
            return (T)Cache[key];
        }
    }
}
