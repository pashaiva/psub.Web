using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared.Abstract;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogDetailsHandler : IQueryHandler<CatalogDetailsQuery, CatalogDetailsViewModel>
    {
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public CatalogDetailsHandler(IRepository<Catalog> catalogRepository,
            IUserService userService,
            IFileService fileService)
        {
            _catalogRepository = catalogRepository;
            _userService = userService;
            _fileService = fileService;
        }

        public CatalogDetailsViewModel Handle(CatalogDetailsQuery catalog)
        {
            var currentDocument = _catalogRepository.Get(catalog.Id);

            if (currentDocument == null)
                return null;

            var result = Mapper.Map<Catalog, CatalogDetailsViewModel>(currentDocument);

            result.IsEditor = _userService.IsAdmin();

            result.Files = _fileService.GetFiles(typeof (Catalog).Name, result.Id);

            return result;
        }
    }
}
