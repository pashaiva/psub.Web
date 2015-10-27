using System.Collections.Generic;
using Psub.DataService.DTO;

namespace Psub.DataService.Abstract
{
    public interface ICatalogSectionService
    {
        PublicationSectionDTO GetCatalogSectionDTOById(int id);
        PublicationSectionDTO GetCatalogSectionDTOByName(string name);
        IEnumerable<PublicationSectionDTO> GetCatalogSectionDTOList();
        int SaveOrUpdate(PublicationSectionDTO catalogSectionDTO);
        void Delete(int id);
    }
}
