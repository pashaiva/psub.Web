using Psub.DataService.HandlerPerQuery.CatalogCommentProcess.Entities;

namespace Psub.DataService.Abstract
{
    public interface ICatalogCommentService
    {
        CatalogCommentListItem Save(int objectId, string text, int commentId);
        ListCatalogComment GetCatalogCommentsByCatalogId(int id, int pageSize, int pageNumber);
    }
}
