using System.Collections.Generic;
using Psub.DataService.DTO;

namespace Psub.DataService.Abstract
{
    public interface IPublicationMainSectionService
    {
        PublicationMainSectionDTO GetPublicationSectionDTOById(int id);
        PublicationMainSectionDTO GetPublicationSectionDTOByName(string name);
        IEnumerable<PublicationMainSectionDTO> GetPublicationSectionDTOList();
        int SaveOrUpdate(PublicationMainSectionDTO publicationMainSectionDTO);
        void Delete(int id);
    }
}
