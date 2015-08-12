namespace Infrastructure.Cache
{
    public class DesktopUserCacheProvider : IUserCacheProvider
    {
        public void Put(string key, object data)
        {
        }

        public T Get<T>(string key)
        {
            return default(T);
        }
    }
}
