using System.Collections.Generic;
using Psub.DataService.DTO;

namespace Psub.DataService.Abstract
{
    public interface ICatalogMainSectionService
    {
        PublicationMainSectionDTO GetCatalogSectionDTOById(int id);
        PublicationMainSectionDTO GetCatalogSectionDTOByName(string name);
        IEnumerable<PublicationMainSectionDTO> GetCatalogSectionDTOList();
        int SaveOrUpdate(PublicationMainSectionDTO catalogMainSectionDTO);
        void Delete(int id);
    }
}
