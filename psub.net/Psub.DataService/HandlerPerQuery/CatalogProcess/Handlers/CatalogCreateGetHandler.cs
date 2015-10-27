using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.CommonViewModels;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Handlers
{
    public class CatalogCreateGetHandler : IQueryHandler<CatalogCreateGetQuery, CatalogCreateGetViewModel>
    {
        private readonly IRepository<CatalogSection> _sectionRepository;

        public CatalogCreateGetHandler(IRepository<CatalogSection> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public CatalogCreateGetViewModel Handle(CatalogCreateGetQuery catalog)
        {
            return new CatalogCreateGetViewModel
                {
                    Section = new DropDownSelectorViewModel
                        {
                            Items = _sectionRepository.Query().Select(m => new DropDownItem
                            {
                                Id = m.Id,
                                Name = m.Name
                            }).ToList()
                        }
                };
        }
    }
}
