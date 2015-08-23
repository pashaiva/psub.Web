using System.Collections.Generic;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogCommentProcess.Entities
{
    public class CatalogCommentListQuery : ListQuery, IQuery<ListCatalogComment>
    {
        public int Id { get; set; }
    }


    public class ListCatalogComment : PagingListQueryResult<CatalogCommentListItem>
    {
        public bool IsEditor { get; set; }
        public List<string> LikeUsers { get; set; }
    }

    public class CatalogCommentListItem
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public string CanReply { get; set; }
        public string Date { get; set; }
        public string ParentId { get; set; }
        public string UserAvatar { get; set; }
        public string CanDelete { get; set; }
        public string Guid { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public string LikeUsers { get; set; }
        public string DisLikeUsers { get; set; }

        public List<CatalogCommentListItem> Replys { get; set; }
    }
}
