using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class SectionEditPostHandler : IQueryHandler<SectionEditPostQuery, SectionEditPostViewModel>
    {
        private readonly IRepository<PublicationSection> _sectionRepository;

        public SectionEditPostHandler(IRepository<PublicationSection> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public SectionEditPostViewModel Handle(SectionEditPostQuery query)
        {
            return new SectionEditPostViewModel
               {
                   Id = _sectionRepository.SaveOrUpdate(new PublicationSection
                       {
                           Id = query.Id,
                           Name = query.Name,
                           PublicationMainSection = new PublicationMainSection { Id = query.MainSectionId }
                       })

               };
        }
    }
}
