using System;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class SectionDeleteGetHandler : IQueryHandler<SectionDeleteGetQuery, SectionDeleteGetViewModel>
    {
        private readonly IRepository<MainSection> _mainSectionRepository;
        private readonly IRepository<Section> _sectionRepository;
        private readonly IRepository<Publication> _publicationRepository;

        public SectionDeleteGetHandler(IRepository<MainSection> mainSectionRepository,
            IRepository<Section> sectionRepository,
            IRepository<Publication> publicationRepository)
        {
            _mainSectionRepository = mainSectionRepository;
            _sectionRepository = sectionRepository;
            _publicationRepository = publicationRepository;
        }

        public SectionDeleteGetViewModel Handle(SectionDeleteGetQuery query)
        {
            var publications = _publicationRepository.Query().Where(m => m.Section != null && m.Section.Id == query.Id);

            if (!publications.Any())
            {
                try
                {
                    _sectionRepository.Delete(query.Id);
                    return new SectionDeleteGetViewModel { Result = true };
                }
                catch (Exception)
                {
                    return new SectionDeleteGetViewModel { Result = false };
                }
            }

            var index = 1;

            return new SectionDeleteGetViewModel { Result = false, Message = Enumerable.Aggregate(publications, string.Empty, (current, publication) => string.Format("{0}{1};   ", current, string.Format("{0}. {1}", index++, publication.TitleText))) };
        }
    }
}
