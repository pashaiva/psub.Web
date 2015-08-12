using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class MainSectionCreatePostHandler : IQueryHandler<MainSectionCreatePostQuery, MainSectionCreatePostViewModel>
    {
        private readonly IRepository<PublicationMainSection> _mainSectionRepository;

        public MainSectionCreatePostHandler(IRepository<PublicationMainSection> mainSectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
        }

        public MainSectionCreatePostViewModel Handle(MainSectionCreatePostQuery query)
        {
            return new MainSectionCreatePostViewModel
               {
                   Id = _mainSectionRepository.SaveOrUpdate(new PublicationMainSection
                       {
                           Name = query.Name
                       })

               };
        }
    }
}
