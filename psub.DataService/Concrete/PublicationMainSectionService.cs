using System;
using System.Collections.Generic;
using System.Linq;
using Psub.DTO.Entities;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class PublicationMainSectionService : IPublicationMainSectionService
    {
        private readonly IRepository<PublicationMainSection> _publicationSelectionRepository;

        public PublicationMainSectionService(IRepository<PublicationMainSection> publicationSelectionRepository)
        {
            _publicationSelectionRepository = publicationSelectionRepository;
        }

        public PublicationMainSectionDTO GetPublicationSectionDTOById(int id)
        {
            var publicationSectionDTO = _publicationSelectionRepository.Get(id);
            return new PublicationMainSectionDTO { Id = publicationSectionDTO.Id, Name = publicationSectionDTO.Name };
        }

        public PublicationMainSectionDTO GetPublicationSectionDTOByName(string name)
        {
            var publicationSectionDTO = _publicationSelectionRepository.Query().FirstOrDefault(m => m.Name == name);
            if (publicationSectionDTO != null)
                return new PublicationMainSectionDTO { Id = publicationSectionDTO.Id, Name = publicationSectionDTO.Name };
            return new PublicationMainSectionDTO();
        }

        public IEnumerable<PublicationMainSectionDTO> GetPublicationSectionDTOList()
        {
            
            return _publicationSelectionRepository.Query().Select(m => new PublicationMainSectionDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    PublicationSection = m.PublicationSections.Select(publicationSections => new PublicationSectionDTO
                        {
                            Id = publicationSections.Id,
                            Name = publicationSections.Name
                        }).ToList()
                }).ToList();
        }

        public int SaveOrUpdate(PublicationMainSectionDTO publicationMainSectionDTO)
        {
            return _publicationSelectionRepository.SaveOrUpdate(new PublicationMainSection
                {
                    Id = 0,
                    Name = publicationMainSectionDTO.Name
                });
        }

        public void Delete(int id)
        {
            //if (_publicationSelectionRepository.Get(id) != null)
                _publicationSelectionRepository.Delete(id);
        }
    }
}
