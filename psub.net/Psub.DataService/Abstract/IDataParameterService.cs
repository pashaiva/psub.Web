using System.Collections.Generic;
using Psub.DataService.DTO;
using Psub.Domain.Entities;

namespace Psub.DataService.Abstract
{
   public interface IDataParameterService
   {
       DataParameterDTO GetDataParameterDTOById(int id);
       List<DataParameterDTO> GetDataParameterDTOListByControlObjectId(int id);
       int SaveOrUpdate(DataParameterDTO dataParameterDTO);
       void SaveDataParameters(List<DataParameterDTO> dataParameterDTO, string guid);
       void Delete(int id);
       List<DataParameter> GetDataParameterByControlObjectGuid(string guid);
       List<FullDataParameter> GetFullDataParameterListByName(string name);
       void ClineFullDataParameterByName(int controlObjectId, string name);
   }
}
