using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class SectionEditPostHandler : IQueryHandler<SectionEditPostQuery, SectionEditPostViewModel>
    {
        private readonly IRepository<Section> _sectionRepository;

        public SectionEditPostHandler(IRepository<Section> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public SectionEditPostViewModel Handle(SectionEditPostQuery query)
        {
            return new SectionEditPostViewModel
               {
                   Id = _sectionRepository.SaveOrUpdate(new Section
                       {
                           Id = query.Id,
                           Name = query.Name,
                           MainSection = new MainSection { Id = query.MainSectionId }
                       })

               };
        }
    }
}
