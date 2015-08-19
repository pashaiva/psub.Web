using System.Collections.Generic;
using System.Linq;
using Psub.DTO.Entities;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class CatalogSectionService : ICatalogSectionService
    {
        private readonly IRepository<CatalogSection> _catalogSelectionRepository;

        public CatalogSectionService(IRepository<CatalogSection> catalogSelectionRepository)
        {
            _catalogSelectionRepository = catalogSelectionRepository;
        }

        public PublicationSectionDTO GetCatalogSectionDTOById(int id)
        {
            var catalogSectionDTO = _catalogSelectionRepository.Get(id);
            return new PublicationSectionDTO { Id = catalogSectionDTO.Id, Name = catalogSectionDTO.Name };
        }

        public PublicationSectionDTO GetCatalogSectionDTOByName(string name)
        {
            var catalogSectionDTO = _catalogSelectionRepository.Query().FirstOrDefault(m => m.Name == name);
            if (catalogSectionDTO != null)
                return new PublicationSectionDTO { Id = catalogSectionDTO.Id, Name = catalogSectionDTO.Name };
            return new PublicationSectionDTO();
        }

        public IEnumerable<PublicationSectionDTO> GetCatalogSectionDTOList()
        {
            return _catalogSelectionRepository.Query().Select(m => new PublicationSectionDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    PublicationMainSection = new PublicationMainSectionDTO
                        {
                            Id = m.MainSection.Id,
                            Name = m.MainSection.Name
                        }
                }).ToList();
        }

        public int SaveOrUpdate(PublicationSectionDTO publicationSectionDTO)
        {
            return _catalogSelectionRepository.SaveOrUpdate(new CatalogSection
                {
                    Id = 0,
                    Name = publicationSectionDTO.Name,
                    MainSection = new CatalogMainSection { Id = publicationSectionDTO.PublicationMainSection.Id }
                });
        }

        public void Delete(int id)
        {
            // if (_publicationSelectionRepository.Get(id) != null)
            _catalogSelectionRepository.Delete(id);
        }
    }
}
