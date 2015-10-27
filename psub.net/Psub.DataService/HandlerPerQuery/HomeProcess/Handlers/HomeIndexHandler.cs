using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.DataService.HandlerPerQuery.HomeProcess.Entities;
using psub.net.Shared.Abstract;

namespace Psub.DataService.HandlerPerQuery.HomeProcess.Handlers
{
    public class HomeIndexHandler : IQueryHandler<HomeIndexQuery, HomeIndexViewModel>
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public HomeIndexHandler(IRepository<Publication> publicationRepository,
            IRepository<Catalog> catalogRepository,
            IUserService userService,
            IFileService fileService)
        {
            _publicationRepository = publicationRepository;
            _catalogRepository = catalogRepository;
            _userService = userService;
            _fileService = fileService;
        }

        public HomeIndexViewModel Handle(HomeIndexQuery catalog)
        {
            var result = new HomeIndexViewModel { Publications = new List<PublicationListItem>(), Catalogs = new List<CatalogListItem>() };
            var returnPublication = new List<PublicationListItem>();

            foreach (var publicationListItem in _publicationRepository.Query().OrderByDescending(m => m.Created).Take(5))
            {
                var returnPublicationItem = Mapper.Map<Publication, PublicationListItem>(publicationListItem);
                returnPublicationItem.IsView = true;
                result.Publications.Add(returnPublicationItem);
            }

            foreach (var catalogListItem in _catalogRepository.Query().OrderByDescending(m => m.Created).Take(5))
            {
                var returnCatalogItem = Mapper.Map<Catalog, CatalogListItem>(catalogListItem);
                returnCatalogItem.IsView = true;
                returnCatalogItem.Files = _fileService.GetFiles(typeof(Catalog).Name, returnCatalogItem.Id);
                result.Catalogs.Add(returnCatalogItem);
            }

            return result;
        }
    }
}
