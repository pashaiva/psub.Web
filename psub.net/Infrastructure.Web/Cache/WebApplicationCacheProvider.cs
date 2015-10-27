using System;
using System.Web;
using System.Web.Caching;
using Infrastructure.Cache;
using Infrastructure.Configuration;

namespace Infrastructure.Web.Cache
{
    public class WebApplicationCacheProvider : IApplicationCacheProvider
    {
        private readonly System.Web.Caching.Cache _cache;

        public WebApplicationCacheProvider()
        {
            _cache = HttpContext.Current.Cache;
        }

        public void Put(string key, object data)
        {
            _cache.Add(key, data, null, DateTime.Now.AddMinutes(Configurations.CacheExpirationInMinutes), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public void Put(string key, object data, int timespanInMinutes)
        {
            _cache.Add(key, data, null, DateTime.Now.AddMinutes(timespanInMinutes), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public T Get<T>(string key)
        {
            if (_cache[key] == null)
            {
                return default(T);
            }
            return (T)_cache[key];
        }
    }
}
