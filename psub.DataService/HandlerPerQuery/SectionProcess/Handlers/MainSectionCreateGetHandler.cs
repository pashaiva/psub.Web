using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class MainSectionCreateGetHandler : IQueryHandler<MainSectionCreateGetQuery, MainSectionCreateGetViewModel>
    {
        private readonly IRepository<MainSection> _mainSectionRepository;

        public MainSectionCreateGetHandler(IRepository<MainSection> mainSectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
        }

        public MainSectionCreateGetViewModel Handle(MainSectionCreateGetQuery catalog)
        {
            return new MainSectionCreateGetViewModel();
        }
    }
}
