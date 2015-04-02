using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Psub.DTO.Entities;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;
using NHibernate.Linq;

namespace Psub.DataService.Concrete
{
    public class RelayDataService : IRelayDataService
    {
        private readonly IRepository<RelayData> _relayDataRepository;
        private readonly IRepository<ControlObject> _controlObjectRepository;
        private readonly IActionLogService _actionLogService;

        public RelayDataService(IRepository<RelayData> relayDataRepository,
            IRepository<ControlObject> controlObjectRepository,
            IActionLogService actionLogService)
        {
            _relayDataRepository = relayDataRepository;
            _controlObjectRepository = controlObjectRepository;
            _actionLogService = actionLogService;
        }

        public int SaveOrUpdate(RelayDataDTO relayDataDTO)
        {
            var relayData = relayDataDTO.Id == 0 ? new RelayData() : _relayDataRepository.Get(relayDataDTO.Id);

            relayData.Id = relayDataDTO.Id;
            var isChangeVal = relayDataDTO.Id == 0 || relayDataDTO.Value != relayData.Value;
            if (isChangeVal)
                relayData.LastUpdate = DateTime.Now;
            if (!string.IsNullOrEmpty(relayDataDTO.Name))
                relayData.Name = relayDataDTO.Name;
            if (relayDataDTO.PinName != 0)
                relayData.PinName = relayDataDTO.PinName;
            relayData.Value = relayDataDTO.Value;
            if (relayDataDTO.ControlObject != null)
                relayData.ControlObject = new ControlObject { Id = relayDataDTO.ControlObject.Id };

            var id = _relayDataRepository.SaveOrUpdate(relayData);

            if (isChangeVal)
                _actionLogService.SetActionLog(string.Format("{0} {1} '{2}'",relayDataDTO.Id > 0 ? (relayData.Value ? "включил(а)" : "отключил(а)") : "", relayDataDTO.Id > 0 ? "исполнительную систему" : "создал(а) исполнительную систему", relayData.Name), id, typeof(RelayData).Name);

            return id;
        }

        public void SaveOrUpdateList(List<RelayDataDTO> relayDataDTO)
        {
            foreach (var relayData in relayDataDTO)
            {
                SaveOrUpdate(relayData);
            }
        }

        public int UpdateBySMS(string controlObjectGuid, string name,string val)
        {
            var raley =
                _relayDataRepository.Query().Fetch(x=>x.ControlObject).First(m => m.ControlObject.Guid == controlObjectGuid && m.PinName == Convert.ToInt32(name));
            raley.Value = val == "1";
            var id = _relayDataRepository.SaveOrUpdate(raley);

            _actionLogService.SetActionLog(string.Format("{0} {1} '{2}'", raley.Id > 0 ? (raley.Value ? "включил(а)" : "отключил(а)") : "", raley.Id > 0 ? "исполнительную систему" : "создал(а) исполнительную систему", raley.Name), id, typeof(RelayData).Name);

            return id;
        }

        public List<RelayDataDTO> GetRelayDataDTOByControlObjectId(int id)
        {
            return _relayDataRepository.Query().Where(m => m.ControlObject.Id == id).Select(m => new RelayDataDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    PinName = m.PinName,
                    Value = m.Value,
                    LastUpdate = m.LastUpdate.ToString(CultureInfo.InvariantCulture)
                }).ToList();
        }

        public List<RelayDataDTO> GetRelayDataDTOByControlObjectGuid(string guid)
        {
            return _relayDataRepository.Query().Where(m => m.ControlObject.Guid == guid).Select(m => new RelayDataDTO
            {
                Id = m.Id,
                Name = m.Name,
                PinName = m.PinName,
                Value = m.Value,
                LastUpdate = m.LastUpdate.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public RelayDataDTO GetRelayDataDTOById(int id)
        {
            var relayData = _relayDataRepository.Get(id);
            return new RelayDataDTO
                {
                    Id = relayData.Id,
                    Name = relayData.Name,
                    PinName = relayData.PinName,
                    Value = relayData.Value,
                    LastUpdate = relayData.LastUpdate.ToString(CultureInfo.InvariantCulture),
                    ControlObject = new ControlObjectDTO { Id = _controlObjectRepository.Query().First(m => m.RelayDatas.Select(x => x.Id).Contains(id)).Id }
                };
        }

        public int GetControlObjectIdByRelayDataId(int id)
        {
            return _relayDataRepository.Get(id).ControlObject.Id;
        }

        public void Delete(int id)
        {
            _relayDataRepository.Delete(id);
        }
    }
}
