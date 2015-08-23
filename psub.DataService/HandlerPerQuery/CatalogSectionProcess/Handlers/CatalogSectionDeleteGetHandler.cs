using System;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Handlers
{
    public class CatalogSectionDeleteGetHandler : IQueryHandler<CatalogSectionDeleteGetQuery, CatalogSectionDeleteGetViewModel>
    {
        private readonly IRepository<CatalogSection> _sectionRepository;
        private readonly IRepository<Catalog> _publicationRepository;

        public CatalogSectionDeleteGetHandler(IRepository<CatalogSection> sectionRepository,
            IRepository<Catalog> publicationRepository)
        {
            _sectionRepository = sectionRepository;
            _publicationRepository = publicationRepository;
        }

        public CatalogSectionDeleteGetViewModel Handle(CatalogSectionDeleteGetQuery catalog)
        {
            var publications = _publicationRepository.Query().Where(m => m.Section != null && m.Section.Id == catalog.Id);

            if (!publications.Any())
            {
                try
                {
                    _sectionRepository.Delete(catalog.Id);
                    return new CatalogSectionDeleteGetViewModel { Result = true };
                }
                catch (Exception)
                {
                    return new CatalogSectionDeleteGetViewModel { Result = false };
                }
            }

            var index = 1;

            return new CatalogSectionDeleteGetViewModel { Result = false, Message = Enumerable.Aggregate(publications, string.Empty, (current, publication) => string.Format("{0}{1};   ", current, string.Format("{0}. {1}", index++, publication.TitleText))) };
        }
    }
}
