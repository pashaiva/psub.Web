using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Psub.DataService.DTO;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class ControlObjectService : IControlObjectService
    {
        private readonly IRepository<ControlObject> _controlObjectRepository;
        private readonly IDataParameterService _dataParameterService;
        private readonly IRelayDataService _relayDataService;
        private readonly IActionLogService _actionLogService;

        public ControlObjectService(IRepository<ControlObject> controlObjectRepository,
            IDataParameterService dataParameterService,
            IRelayDataService relayDataService,
             IActionLogService actionLogService)
        {
            _controlObjectRepository = controlObjectRepository;
            _dataParameterService = dataParameterService;
            _relayDataService = relayDataService;
            _actionLogService = actionLogService;
        }

        public ControlObjectDTO GetControlObjectById(int id)
        {
            var controlObject = _controlObjectRepository.Get(id);

            var controlObjectDTO = new ControlObjectDTO
                {
                    Id = controlObject.Id,
                    Name = controlObject.Name,
                    Discription = controlObject.Discription,
                    DataParameters = _dataParameterService.GetDataParameterDTOListByControlObjectId(controlObject.Id),
                    RelayData = _relayDataService.GetRelayDataDTOByControlObjectId(controlObject.Id),
                    Guid = controlObject.Guid,
                    Client = new ClientDTO
                        {
                            Id = controlObject.Client.Id,
                            Name = controlObject.Client.Name,
                            Discription = controlObject.Client.Discription,
                            Guid = controlObject.Client.Guid
                        }
                };
            return controlObjectDTO;
        }

        public List<ControlObjectDTO> GetControlObjectList(UserDTO user)
        {
            var controlObject = _controlObjectRepository.Query().Fetch(x => x.Client).ToList().Where(m => m.Client.Users.ToList().Select(x => x.UserGuid).Contains(user.UserGuid));

            return controlObject.Select(item => new ControlObjectDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Discription = item.Discription,
                    DataParameters = _dataParameterService.GetDataParameterDTOListByControlObjectId(item.Id),
                    RelayData = _relayDataService.GetRelayDataDTOByControlObjectId(item.Id),
                    Guid = item.Guid,
                    Client = new ClientDTO
                        {
                            Id = item.Client.Id,
                            Name = item.Client.Name,
                            Discription = item.Client.Discription,
                            Guid = item.Client.Guid
                        }
                }).ToList();
        }

        public void SaveOrUpdate(ControlObjectDTO controlObjectDTO)
        {
            var controlObject = controlObjectDTO.Id > 0
                                    ? _controlObjectRepository.Get(controlObjectDTO.Id)
                                    : new ControlObject();

            controlObject.Name = controlObjectDTO.Name;
            controlObject.Discription = controlObjectDTO.Discription;

            var id=_controlObjectRepository.SaveOrUpdate(controlObject);
            _actionLogService.SetActionLog(controlObjectDTO.Id > 0 ? "редактировал(а) объект контроля " : "создал(а) объект контроля ", id, typeof(ControlObject).Name);
        }
    }
}
