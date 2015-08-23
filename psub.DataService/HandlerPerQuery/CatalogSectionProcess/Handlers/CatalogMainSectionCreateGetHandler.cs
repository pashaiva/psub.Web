using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Handlers
{
    public class CatalogMainSectionCreateGetHandler : IQueryHandler<CatalogMainSectionCreateGetQuery, CatalogMainSectionCreateGetViewModel>
    {
        private readonly IRepository<CatalogMainSection> _mainSectionRepository;

        public CatalogMainSectionCreateGetHandler(IRepository<CatalogMainSection> mainSectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
        }

        public CatalogMainSectionCreateGetViewModel Handle(CatalogMainSectionCreateGetQuery catalog)
        {
            return new CatalogMainSectionCreateGetViewModel();
        }
    }
}
