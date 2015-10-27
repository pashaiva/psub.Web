using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using psub.net.Shared;
using psub.net.Shared.Abstract;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogListHandler : IQueryHandler<CatalogListQuery, ListCatalog>
    {
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public CatalogListHandler(IRepository<Catalog> catalogRepository,
            IUserService userService,
            IFileService fileService)
        {
            _catalogRepository = catalogRepository;
            _userService = userService;
            _fileService = fileService;
        }

        public ListCatalog Handle(CatalogListQuery catalog)
        {
            var pageSize = catalog.PageSize > 0 ? catalog.PageSize : ConfigData.PageSize;
            var catalogs = _catalogRepository.Query().Where(m => catalog.CatalogSectionId == 0 || (m.Section != null && m.Section.Id == catalog.CatalogSectionId)).OrderByDescending(m => m.Id);
            var currentUser = _userService.GetCurrentUser();
            var selectCatalogs = catalogs.Skip((catalog.Page - 1) * pageSize).Take(pageSize).ToList();
            var returnCatalog = new List<CatalogListItem>();

            foreach (var catalogListItem in selectCatalogs)
            {
                var returnCatalogItem = Mapper.Map<Catalog, CatalogListItem>(catalogListItem);
                returnCatalogItem.IsView = true;
                returnCatalogItem.Files = _fileService.GetFiles(typeof(Catalog).Name, returnCatalogItem.Id);
                returnCatalog.Add(returnCatalogItem);
            }

            var result = new ListCatalog
            {
                Items = returnCatalog,
                Count = catalogs.Count(),
                PageNumber = catalog.Page,
                PageSize = pageSize,
                IsEditor = _userService.IsAdmin(),

            };

            return result;
        }
    }
}
