using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogDetailsHandler : IQueryHandler<CatalogDetailsQuery, CatalogDetailsViewModel>
    {
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IUserService _userService;

        public CatalogDetailsHandler(IRepository<Catalog> catalogRepository,
            IUserService userService)
        {
            _catalogRepository = catalogRepository;
            _userService = userService;
        }

        public CatalogDetailsViewModel Handle(CatalogDetailsQuery catalog)
        {
            var currentDocument = _catalogRepository.Get(catalog.Id);

            if (currentDocument == null)
                return null;

            var result = Mapper.Map<Catalog, CatalogDetailsViewModel>(currentDocument);

            result.IsEditor = _userService.IsAdmin();

            return result;
        }
    }
}
