using System;
using System.Collections.Generic;
using System.Linq;
using Psub.DTO.Entities;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class PublicationSectionService : IPublicationSectionService
    {
        private readonly IRepository<PublicationSection> _publicationSelectionRepository;

        public PublicationSectionService(IRepository<PublicationSection> publicationSelectionRepository)
        {
            _publicationSelectionRepository = publicationSelectionRepository;
        }

        public PublicationSectionDTO GetPublicationSectionDTOById(int id)
        {
            var publicationSectionDTO = _publicationSelectionRepository.Get(id);
            return new PublicationSectionDTO { Id = publicationSectionDTO.Id, Name = publicationSectionDTO.Name };
        }

        public PublicationSectionDTO GetPublicationSectionDTOByName(string name)
        {
            var publicationSectionDTO = _publicationSelectionRepository.Query().FirstOrDefault(m => m.Name == name);
            if (publicationSectionDTO != null)
                return new PublicationSectionDTO { Id = publicationSectionDTO.Id, Name = publicationSectionDTO.Name };
            return new PublicationSectionDTO();
        }

        public IEnumerable<PublicationSectionDTO> GetPublicationSectionDTOList()
        {
            return _publicationSelectionRepository.Query().Select(m => new PublicationSectionDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    PublicationMainSection = new PublicationMainSectionDTO
                        {
                            Id = m.PublicationMainSection.Id,
                            Name = m.PublicationMainSection.Name
                        }
                }).ToList();
        }

        public int SaveOrUpdate(PublicationSectionDTO publicationSectionDTO)
        {
            return _publicationSelectionRepository.SaveOrUpdate(new PublicationSection
                {
                    Id = 0,
                    Name = publicationSectionDTO.Name,
                    PublicationMainSection = new PublicationMainSection { Id = publicationSectionDTO.PublicationMainSection.Id }
                });
        }

        public void Delete(int id)
        {
           // if (_publicationSelectionRepository.Get(id) != null)
                _publicationSelectionRepository.Delete(id);
        }
    }
}
