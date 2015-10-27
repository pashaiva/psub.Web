using System.Collections.Generic;
using Psub.DataService.DTO;

namespace Psub.DataService.Abstract
{
    public interface IControlObjectService
    {
        ControlObjectDTO GetControlObjectById(int id);
        List<ControlObjectDTO> GetControlObjectList(UserDTO user);
        void SaveOrUpdate(ControlObjectDTO controlObjectDTO);
    }
}
