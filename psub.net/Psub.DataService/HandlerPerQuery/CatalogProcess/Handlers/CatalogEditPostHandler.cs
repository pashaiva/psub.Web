using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using psub.net.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogEditPostHandler : IQueryHandler<CatalogEditPostQuery, CatalogEditViewModel>
    {
        private readonly IUserService _userService;
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IActionLogService _actionLogService;
        private readonly IRepository<Section> _sectionRepository;

        public CatalogEditPostHandler(IUserService userService,
            IRepository<Catalog> catalogRepository,
            IActionLogService actionLogService,
            IRepository<Section> sectionRepository)
        {
            _userService = userService;
            _catalogRepository = catalogRepository;
            _actionLogService = actionLogService;
            _sectionRepository = sectionRepository;
        }

        public CatalogEditViewModel Handle(CatalogEditPostQuery catalog)
        {
            var currentUser = _userService.GetCurrentUser();

            if (!_userService.IsAdmin())
                return null;

            var lastCatalog = _catalogRepository.Get(catalog.Id);

            lastCatalog.Text = catalog.Text;
            lastCatalog.TextPreview = catalog.TextPreview;
            lastCatalog.TitleText = catalog.TitleText;
            lastCatalog.Price = catalog.Price;
            lastCatalog.Keywords = catalog.Keywords;
            lastCatalog.IsPublic = catalog.IsPublic;
            lastCatalog.Section = new Section { Id = catalog.Section.Id };

            _catalogRepository.SaveOrUpdate(lastCatalog);

            _actionLogService.SetActionLog("редактировал(а) публикацию", lastCatalog.Id, typeof(Catalog).Name,
                FormatUtlities.FormatVirtualDetailsUrl(typeof(Catalog).Name, lastCatalog.Id, lastCatalog.TitleText));

            return Mapper.Map<Catalog, CatalogEditViewModel>(lastCatalog);
        }
    }
}
