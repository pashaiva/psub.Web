using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Psub.DTO.Entities;
using Psub.DataAccess.Abstract;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
   public class ActionLogService : IActionLogService
    {
        private readonly IRepository<ActionLog> _actionLogRepository;
        private readonly IUserService _userService;

        public ActionLogService(IRepository<ActionLog> actionLogRepository,
            IUserService userService)
        {
            _actionLogRepository = actionLogRepository;
            _userService = userService;
        }

        public ActionLogDTO GetActionLogDTOById(int id)
        {
            var actionLog = _actionLogRepository.Get(id);

            return new ActionLogDTO
            {
                Id = actionLog.Id,
                ActionName = actionLog.ActionName,
                UserName = actionLog.UserName,
                UserGuid = actionLog.UserGuid,
                ObjectId = actionLog.ObjectId,
                Date = actionLog.Date,
                DateJson = string.Format("{0} {1}", actionLog.Date.ToShortDateString(), actionLog.Date.ToShortTimeString()),
                Type = actionLog.Type
            };
        }

        public IEnumerable<ActionLogDTO> GetActionLogDTOList(string objectType, int objectId)
        {
            var list = _actionLogRepository.Query().Where(m => m.ObjectId == objectId && m.Type == objectType)
                .Select(actionLog => new ActionLogDTO
                {
                    Id = actionLog.Id,
                    ActionName = actionLog.ActionName,
                    UserName = actionLog.UserName,
                    UserGuid = actionLog.UserGuid,
                    ObjectId = actionLog.ObjectId,
                    Date = actionLog.Date,
                    DateJson = string.Format("{0} {1}", actionLog.Date.ToShortDateString(), actionLog.Date.ToShortTimeString()),
                    Type = actionLog.Type
                }).OrderBy(m=>m.Date);

            return list;
        }

        public int SaveOrUpdate(ActionLogDTO actionLog)
        {
            var user = _userService.GetCurrentUser();

            return _actionLogRepository.SaveOrUpdate(new ActionLog
            {
                Id = actionLog.Id,
                ActionName = actionLog.ActionName,
                UserName = user.Name,
                UserGuid = user.UserGuid,
                ObjectId = actionLog.ObjectId,
                Date = DateTime.Now,
                Type = actionLog.Type
            });
        }

        public int SaveOrUpdate(ActionLog actionLog)
        {
            return _actionLogRepository.SaveOrUpdate(actionLog);
        }

        public void SetActionLog(string actionName, int objectId, string objectType)
        {
            SaveOrUpdate(new ActionLogDTO
            {
                ActionName = actionName,
                ObjectId = objectId,
                Type = objectType
            });
        }

        public void SetActionLogLocalApp(string actionName, int objectId, string objectType, string userName)
        {
            SaveOrUpdateLocalApp(new ActionLogDTO
            {
                ActionName = actionName,
                ObjectId = objectId,
                Type = objectType,
                UserName = userName
            });
        }

        public int SaveOrUpdateLocalApp(ActionLogDTO actionLog)
        {
            return _actionLogRepository.SaveOrUpdate(new ActionLog
            {
                Id = 0,
                ActionName = actionLog.ActionName,
                UserName = actionLog.UserName,
                ObjectId = actionLog.ObjectId,
                Date = DateTime.Now,
                Type = actionLog.Type
            });
        }

        public IQueryable<ActionLog> GetAllActionLogWithEmptyId()
        {
            return _actionLogRepository.Query().Where(x => x.ObjectId == 0).OrderBy(m => m.Date);
        }
    }
}




