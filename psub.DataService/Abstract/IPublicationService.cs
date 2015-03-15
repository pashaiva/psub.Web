using System.Collections.Generic;
using Psub.DTO.Entities;

namespace Psub.DataService.Abstract
{
    public interface IPublicationService
    {
        IEnumerable<PublicationDTO> GetPublicationList(int publicationSectionId, int publicationMainSectionId);
        int SaveOrUpdate(PublicationDTO publication);
        IEnumerable<PublicationDTO> GetPublicationListTop(int count = 10);
        PublicationDTO GetPublicationById(int id);
        void DeletePublication(int id);
    }
}
