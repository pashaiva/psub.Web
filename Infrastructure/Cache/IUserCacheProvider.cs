namespace Infrastructure.Cache
{
    public interface IUserCacheProvider
    {
        void Put(string key, object data);
        T Get<T>(string key);
    }
}
