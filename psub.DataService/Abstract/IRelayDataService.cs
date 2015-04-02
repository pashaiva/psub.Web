using System.Collections.Generic;
using Psub.DTO.Entities;

namespace Psub.DataService.Abstract
{
    public interface IRelayDataService
    {
        int SaveOrUpdate(RelayDataDTO relayDataDTO);
        void SaveOrUpdateList(List<RelayDataDTO> relayDataDTO);
        List<RelayDataDTO> GetRelayDataDTOByControlObjectId(int id);
        List<RelayDataDTO> GetRelayDataDTOByControlObjectGuid(string guid);
        RelayDataDTO GetRelayDataDTOById(int id);
        int GetControlObjectIdByRelayDataId(int id);
        void Delete(int id);
        int UpdateBySMS(string controlObjectGuid, string name, string val);
    }
}
