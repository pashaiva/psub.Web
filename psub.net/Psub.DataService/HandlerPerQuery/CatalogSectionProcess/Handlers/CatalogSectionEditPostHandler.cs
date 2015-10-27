using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Handlers
{
    public class CatalogSectionEditPostHandler : IQueryHandler<CatalogSectionEditPostQuery, CatalogSectionEditPostViewModel>
    {
        private readonly IRepository<CatalogSection> _sectionRepository;

        public CatalogSectionEditPostHandler(IRepository<CatalogSection> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public CatalogSectionEditPostViewModel Handle(CatalogSectionEditPostQuery catalog)
        {
            return new CatalogSectionEditPostViewModel
               {
                   Id = _sectionRepository.SaveOrUpdate(new CatalogSection
                       {
                           Id = catalog.Id,
                           Name = catalog.Name,
                           MainSection = new CatalogMainSection { Id = catalog.MainSectionId }
                       })

               };
        }
    }
}
