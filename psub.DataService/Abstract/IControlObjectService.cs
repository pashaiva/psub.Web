using System.Collections.Generic;
using Psub.DTO.Entities;

namespace Psub.DataService.Abstract
{
    public interface IControlObjectService
    {
        ControlObjectDTO GetControlObjectById(int id);
        List<ControlObjectDTO> GetControlObjectList(UserDTO user);
        void SaveOrUpdate(ControlObjectDTO controlObjectDTO);
    }
}
