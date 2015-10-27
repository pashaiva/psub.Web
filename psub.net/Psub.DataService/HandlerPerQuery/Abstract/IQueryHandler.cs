namespace UESPDataManager.DataService.HandlerPerQuery.Abstract
{
    public interface IQueryHandler<in TQuery, out TResponse>
        where TQuery : IQuery<TResponse>
    {
        TResponse Handle(TQuery catalog);
    }
}
