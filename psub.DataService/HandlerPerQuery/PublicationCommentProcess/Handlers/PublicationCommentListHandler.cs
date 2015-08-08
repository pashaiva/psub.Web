using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Handlers
{
    public class PublicationCommentListHandler : IQueryHandler<PublicationCommentListQuery, ListPublicationComment>
    {
        private readonly IPublicationCommentService _publicationCommentService;

        public PublicationCommentListHandler(IPublicationCommentService publicationCommentService)
        {
            _publicationCommentService = publicationCommentService;
        }

        public ListPublicationComment Handle(PublicationCommentListQuery query)
        {
            return _publicationCommentService.GetPublicationCommentsByPublicationId(query.Id, query.PageSize > 0 ? query.PageSize : 25, query.Page);
        }
    }
}
