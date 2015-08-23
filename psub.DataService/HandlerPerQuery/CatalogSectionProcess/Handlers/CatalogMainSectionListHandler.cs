using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Handlers
{
    public class CatalogMainSectionListHandler : IQueryHandler<CatalogMainSectionListQuery, ListCatalogMainSection>
    {
        private readonly IRepository<CatalogMainSection> _mainSectionRepository;
        private readonly IUserService _userService;

        public CatalogMainSectionListHandler(IRepository<CatalogMainSection> mainSectionRepository,
            IUserService userService)
        {
            _mainSectionRepository = mainSectionRepository;
            _userService = userService;
        }

        public ListCatalogMainSection Handle(CatalogMainSectionListQuery catalog)
        {
            var mainSections = _mainSectionRepository.Query().ToList();

            return new ListCatalogMainSection
                {
                    Items = Mapper.Map<List<CatalogMainSection>, List<CatalogMainSectionListItem>>(mainSections),
                    IsAdminPublications = _userService.IsAdmin()
                };
        }
    }
}
