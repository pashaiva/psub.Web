using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class SectionCreateGetHandler : IQueryHandler<SectionCreateGetQuery, SectionCreateGetViewModel>
    {
        private readonly IRepository<MainSection> _mainSectionRepository;

        public SectionCreateGetHandler(IRepository<MainSection> mainSectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
        }

        public SectionCreateGetViewModel Handle(SectionCreateGetQuery query)
        {
            return new SectionCreateGetViewModel
               {
                   MainSections = _mainSectionRepository.Query().Select(m => new MainSectionItem
                       {
                           Id = m.Id,
                           Name = m.Name
                       }).ToList()
               };
        }
    }
}
