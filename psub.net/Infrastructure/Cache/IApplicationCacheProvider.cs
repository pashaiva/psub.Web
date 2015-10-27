namespace Infrastructure.Cache
{
    public interface IApplicationCacheProvider
    {
        void Put(string key, object data);
        void Put(string key, object data, int timespanInMinutes);
        T Get<T>(string key);
    }
}
