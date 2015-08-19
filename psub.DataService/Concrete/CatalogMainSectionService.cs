using System.Collections.Generic;
using System.Linq;
using Psub.DTO.Entities;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class CatalogMainSectionService : ICatalogMainSectionService
    {
        private readonly IRepository<CatalogMainSection> _catalogSelectionRepository;

        public CatalogMainSectionService(IRepository<CatalogMainSection> catalogSelectionRepository)
        {
            _catalogSelectionRepository = catalogSelectionRepository;
        }

        public PublicationMainSectionDTO GetCatalogSectionDTOById(int id)
        {
            var catalogSectionDTO = _catalogSelectionRepository.Get(id);
            return new PublicationMainSectionDTO { Id = catalogSectionDTO.Id, Name = catalogSectionDTO.Name };
        }

        public PublicationMainSectionDTO GetCatalogSectionDTOByName(string name)
        {
            var catalogSectionDTO = _catalogSelectionRepository.Query().FirstOrDefault(m => m.Name == name);
            if (catalogSectionDTO != null)
                return new PublicationMainSectionDTO { Id = catalogSectionDTO.Id, Name = catalogSectionDTO.Name };
            return new PublicationMainSectionDTO();
        }

        public IEnumerable<PublicationMainSectionDTO> GetCatalogSectionDTOList()
        {

            return _catalogSelectionRepository.Query().Select(m => new PublicationMainSectionDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    PublicationSection = m.Sections.Select(publicationSections => new PublicationSectionDTO
                        {
                            Id = publicationSections.Id,
                            Name = publicationSections.Name
                        }).ToList()
                }).ToList();
        }

        public int SaveOrUpdate(PublicationMainSectionDTO catalogMainSectionDTO)
        {
            return _catalogSelectionRepository.SaveOrUpdate(new CatalogMainSection
                {
                    Id = 0,
                    Name = catalogMainSectionDTO.Name
                });
        }

        public void Delete(int id)
        {
            //if (_publicationSelectionRepository.Get(id) != null)
            _catalogSelectionRepository.Delete(id);
        }
    }
}
