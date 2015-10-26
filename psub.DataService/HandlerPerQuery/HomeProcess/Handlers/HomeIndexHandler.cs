using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PublicationDetailsHandler : IQueryHandler<PublicationDetailsQuery, PublicationDetailsViewModel>
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IUserService _userService;

        public PublicationDetailsHandler(IRepository<Publication> publicationRepository,
            IUserService userService)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
        }

        public PublicationDetailsViewModel Handle(PublicationDetailsQuery catalog)
        {
            var currentDocument = _publicationRepository.Get(catalog.Id);

            if (currentDocument == null)
                return null;

            var result = Mapper.Map<Publication, PublicationDetailsViewModel>(currentDocument);

            result.IsEditor = _userService.IsAdmin();

            return result;
        }
    }
}
