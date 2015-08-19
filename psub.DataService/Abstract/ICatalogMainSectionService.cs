using System.Collections.Generic;
using Psub.DTO.Entities;

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
