using Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Entities;

namespace Psub.DataService.Abstract
{
    public interface IPublicationCommentService
    {
        PublicationCommentListItem Save(int objectId, string text, int commentId);
        ListPublicationComment GetPublicationCommentsByPublicationId(int id, int pageSize, int pageNumber);
    }
}
