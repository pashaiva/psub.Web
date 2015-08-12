using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogListHandler : IQueryHandler<CatalogListQuery, ListCatalog>
    {
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IUserService _userService;

        public CatalogListHandler(IRepository<Catalog> catalogRepository,
            IUserService userService)
        {
            _catalogRepository = catalogRepository;
            _userService = userService;
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
                returnCatalog.Add(returnCatalogItem);
            }

            var result = new ListCatalog
            {
                Items = returnCatalog,
                Count = catalogs.Count(),
                PageNumber = catalog.Page,
                PageSize = pageSize,
                IsEditor = _userService.IsAdmin()
            };

            return result;
        }
    }
}
