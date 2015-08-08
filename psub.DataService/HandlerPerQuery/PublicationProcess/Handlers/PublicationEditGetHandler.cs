using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PublicationEditGetHandler : IQueryHandler<PublicationEditGetQuery, PublicationEditViewModel>
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IUserService _userService;

        public PublicationEditGetHandler(IRepository<Publication> publicationRepository,
            IUserService userService)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
        }

        public PublicationEditViewModel Handle(PublicationEditGetQuery query)
        {
            var currentDocument = _publicationRepository.Get(query.Id);
            var currentUser = _userService.GetCurrentUser();

            if (currentDocument == null)
                return null;

            var result = Mapper.Map<Publication, PublicationEditViewModel>(currentDocument);

            return result;
        }
    }
}
