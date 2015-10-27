using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.HomeProcess.Handlers
{
    public class HomeIndexHandler : IQueryHandler<HomeIndexQuery, HomeIndexViewModel>
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IUserService _userService;

        public HomeIndexHandler(IRepository<Publication> publicationRepository,
            IUserService userService)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
        }

        public HomeIndexViewModel Handle(HomeIndexQuery catalog)
        {
            var result = new HomeIndexViewModel();
            var currentDocument = _publicationRepository.Get(catalog.Id);

            if (currentDocument == null)
                return null;

            //var result = Mapper.Map<Publication, PublicationDetailsViewModel>(currentDocument);
            
            return result;
        }
    }
}
