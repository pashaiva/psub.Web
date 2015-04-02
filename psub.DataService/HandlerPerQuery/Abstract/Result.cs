using System.Collections.Generic;

namespace UESPDataManager.DataService.HandlerPerQuery.Abstract
{
    public abstract class QueryResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public abstract class PagingListQueryResult<TListItem>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<TListItem> Items { get; set; }
    }
}
