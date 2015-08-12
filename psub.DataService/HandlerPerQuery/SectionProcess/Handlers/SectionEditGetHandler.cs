using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class SectionEditGetHandler : IQueryHandler<SectionEditGetQuery, SectionEditGetViewModel>
    {
        private readonly IRepository<PublicationMainSection> _mainSectionRepository;
        private readonly IRepository<PublicationSection> _sectionRepository;

        public SectionEditGetHandler(IRepository<PublicationMainSection> mainSectionRepository,
            IRepository<PublicationSection> sectionRepository)
        {
            _mainSectionRepository = mainSectionRepository;
            _sectionRepository = sectionRepository;
        }

        public SectionEditGetViewModel Handle(SectionEditGetQuery query)
        {
            var section = _sectionRepository.Get(query.Id);

            return new SectionEditGetViewModel
               {
                   Id = section.Id,
                   Name = section.Name,
                   MainSectionId = section.PublicationMainSection.Id,
                   MainSections = _mainSectionRepository.Query().Select(m => new MainSectionItem
                       {
                           Id = m.Id,
                           Name = m.Name
                       }).ToList()
               };
        }
    }
}
