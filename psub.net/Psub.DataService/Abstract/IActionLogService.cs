using System.Collections.Generic;
using System.Linq;
using Psub.DataService.DTO;
using Psub.Domain.Entities;

namespace Psub.DataService.Abstract
{
    public interface IActionLogService
    {
        ActionLogDTO GetActionLogDTOById(int id);
        IEnumerable<ActionLogDTO> GetActionLogDTOList(string objectType, int objectId);
        int SaveOrUpdate(ActionLogDTO actionLog);
        int SaveOrUpdate(ActionLog actionLog);
        void SetActionLog(string actionName, int objectId, string objectType, string objectName = "");
        void SetActionLogLocalApp(string actionName, int objectId, string objectType, string userName);
        int SaveOrUpdateLocalApp(ActionLogDTO actionLog);
        IQueryable<ActionLog> GetAllActionLogWithEmptyId();
    }
}
