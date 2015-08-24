using System;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogCreateHandler : IQueryHandler<CatalogCreateQuery, CatalogCreateResult>
    {
        private readonly IRepository<Catalog> _catalogRepository;
        private readonly IUserService _userService;
        private readonly IActionLogService _actionLogService;

        public CatalogCreateHandler(IRepository<Catalog> catalogRepository,
            IUserService userService,
            IActionLogService actionLogService)
        {
            _catalogRepository = catalogRepository;
            _userService = userService;
            _actionLogService = actionLogService;
        }

        public CatalogCreateResult Handle(CatalogCreateQuery catalog)
        {
            var currentUser = _userService.GetCurrentUser();
            var saveCatalog = new Catalog
            {
                UserGuid = currentUser.UserGuid,
                UserName = currentUser.Name,
                Created = DateTime.Now,
                Keywords = catalog.Keywords,
                Text = catalog.Text,
                TextPreview = catalog.TextPreview,
                TitleText = catalog.TitleText,
                Price = catalog.Price,
                IsPublic = catalog.IsPublic,
                Section = new Section { Id = catalog.Section.Id }
            };

            var id = _catalogRepository.SaveOrUpdate(saveCatalog);

            _actionLogService.SetActionLog("создал(а) публикацию", id, typeof(Catalog).Name,
                FormatUtlities.FormatVirtualDetailsUrl(typeof(Catalog).Name, saveCatalog.Id, saveCatalog.TitleText));

            return new CatalogCreateResult { Id = id };
        }
    }
}
