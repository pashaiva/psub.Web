using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Handlers
{
    public class CatalogSectionEditGetHandler : IQueryHandler<CatalogSectionEditGetQuery, CatalogSectionEditGetViewModel>
    {
        private readonly IRepository<CatalogMainSection> _mainSectionRepository;
        private readonly IRepository<CatalogSection> _sectionRepository;

        public CatalogSectionEditGetHandler(IRepository<CatalogMainSection> mainSectionRepository,
            IRepository<CatalogSection> sectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
            _sectionRepository = sectionRepository;
        }

        public CatalogSectionEditGetViewModel Handle(CatalogSectionEditGetQuery catalog)
        {
            var section = _sectionRepository.Get(catalog.Id);

            return new CatalogSectionEditGetViewModel
               {
                   Id = section.Id,
                   Name = section.Name,
                   MainSectionId = section.MainSection.Id,
                   MainSections = _mainSectionRepository.Query().Select(m => new CatalogMainSectionItem
                       {
                           Id = m.Id,
                           Name = m.Name
                       }).ToList()
               };
        }
    }
}
