using System.Web;
using Infrastructure.Cache;

namespace Infrastructure.Web.Cache
{
    public class WebUserCacheProvider : IUserCacheProvider
    {
        public void Put(string key, object data)
        {
            HttpContext.Current.Session[key] = data;
        }

        public T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null)
            {
                return default(T);
            }
            return (T)HttpContext.Current.Session[key];
        }
    }
}
