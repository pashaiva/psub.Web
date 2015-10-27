namespace UESPDataManager.DataService.HandlerPerQuery.Abstract
{
    public interface IQuery<TResponse> { }

    public abstract class ListQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
