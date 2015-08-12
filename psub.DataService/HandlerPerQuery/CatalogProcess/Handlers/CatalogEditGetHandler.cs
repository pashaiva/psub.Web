using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.CommonViewModels;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using System.Linq;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogEditGetHandler : IQueryHandler<CatalogEditGetQuery, CatalogEditViewModel>
    {
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IUserService _userService;
        private readonly IRepository<Section> _sectionRepository;

        public CatalogEditGetHandler(IRepository<Catalog> catalogRepository,
            IUserService userService,
            IRepository<Section> sectionRepository)
        {
            _catalogRepository = catalogRepository;
            _userService = userService;
            _sectionRepository = sectionRepository;
        }

        public CatalogEditViewModel Handle(CatalogEditGetQuery catalog)
        {
            var currentDocument = _catalogRepository.Get(catalog.Id);

            if (currentDocument == null || !_userService.IsAdmin())
                return null;

            var result = Mapper.Map<Catalog, CatalogEditViewModel>(currentDocument);

            result.Section = new DropDownSelectorViewModel
                {
                    Items = _sectionRepository.Query().Select(m => new DropDownItem()
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
                    Id = result.Section.Id
                };

            return result;
        }
    }
}
