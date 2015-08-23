using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Handlers
{
    public class CatalogMainSectionCreatePostHandler : IQueryHandler<CatalogMainSectionCreatePostQuery, CatalogMainSectionCreatePostViewModel>
    {
        private readonly IRepository<CatalogMainSection> _mainSectionRepository;

        public CatalogMainSectionCreatePostHandler(IRepository<CatalogMainSection> mainSectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
        }

        public CatalogMainSectionCreatePostViewModel Handle(CatalogMainSectionCreatePostQuery catalog)
        {
            return new CatalogMainSectionCreatePostViewModel
               {
                   Id = _mainSectionRepository.SaveOrUpdate(new CatalogMainSection
                       {
                           Name = catalog.Name
                       })

               };
        }
    }
}
