using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Handlers
{
    public class CatalogSectionCreateGetHandler : IQueryHandler<CatalogSectionCreateGetQuery, CatalogSectionCreateGetViewModel>
    {
        private readonly IRepository<CatalogMainSection> _mainSectionRepository;

        public CatalogSectionCreateGetHandler(IRepository<CatalogMainSection> mainSectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
        }

        public CatalogSectionCreateGetViewModel Handle(CatalogSectionCreateGetQuery catalog)
        {
            return new CatalogSectionCreateGetViewModel
               {
                   MainSections = _mainSectionRepository.Query().Select(m => new CatalogMainSectionItem
                       {
                           Id = m.Id,
                           Name = m.Name
                       }).ToList()
               };
        }
    }
}
