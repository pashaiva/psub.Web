using System.Collections.Generic;
using Psub.DataService.DTO;

namespace Psub.DataService.Abstract
{
    public interface IPublicationSectionService
    {
        PublicationSectionDTO GetPublicationSectionDTOById(int id);
        PublicationSectionDTO GetPublicationSectionDTOByName(string name);
        IEnumerable<PublicationSectionDTO> GetPublicationSectionDTOList();
        int SaveOrUpdate(PublicationSectionDTO publicationSectionDTO);
        void Delete(int id);
    }
}
