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
    public class DataParameterService : IDataParameterService
    {
        private readonly IRepository<DataParameter> _dataParameterRepository;
        private readonly IRepository<FullDataParameter> _fullDataParameterRepository;
        private readonly IRepository<ControlObject> _controlObjectRepository;
        private readonly IUserService _userService;

        public DataParameterService(IRepository<DataParameter> dataParameterRepository,
            IRepository<ControlObject> controlObjectRepository,
            IUserService userService,
            IRepository<FullDataParameter> fullDataParameterRepository)
        {
            _dataParameterRepository = dataParameterRepository;
            _controlObjectRepository = controlObjectRepository;
            _userService = userService;
            _fullDataParameterRepository = fullDataParameterRepository;
        }

        public DataParameterDTO GetDataParameterDTOById(int id)
        {
            var dataParameter = _dataParameterRepository.Get(id);

            var contrObj = _controlObjectRepository.Get(dataParameter.ControlObject.Id);

            var dataParameterDTO = new DataParameterDTO
                {
                    Id = dataParameter.Id,
                    MeteringType = dataParameter.MeteringType,
                    Name = dataParameter.Name,
                    Value = dataParameter.Value,
                    LastUpdate = dataParameter.LastUpdate.ToString(CultureInfo.InvariantCulture),
                    ControlObject = new ControlObjectDTO
                        {
                            Client = new ClientDTO(),
                            DataParameters = new List<DataParameterDTO>(),
                            Discription = contrObj.Discription,
                            Id = contrObj.Id,
                            Name = contrObj.Name
                        }
                };
            return dataParameterDTO;
        }

        public List<DataParameterDTO> GetDataParameterDTOListByControlObjectId(int id)
        {
            var dataParameter = _dataParameterRepository.Query().Fetch(m => m.ControlObject).Where(x => x.ControlObject.Id == id);

            return dataParameter.Select(parameter => new DataParameterDTO
                {
                    Id = parameter.Id,
                    MeteringType = parameter.MeteringType,
                    Name = parameter.Name,
                    Value = parameter.Value,
                    LastUpdate = parameter.LastUpdate.ToString(CultureInfo.InvariantCulture),
                    ControlObject = new ControlObjectDTO
                    {
                        Client = new ClientDTO(),
                        DataParameters = new List<DataParameterDTO>(),
                        Discription = _controlObjectRepository.Get(parameter.ControlObject.Id).Discription,
                        Id = _controlObjectRepository.Get(parameter.ControlObject.Id).Id,
                        Name = _controlObjectRepository.Get(parameter.ControlObject.Id).Name
                    }
                }).ToList();
        }

        public int SaveOrUpdate(DataParameterDTO dataParameterDTO)
        {
            var dataParameter = dataParameterDTO.Id == 0
                                    ? new DataParameter()
                                    : _dataParameterRepository.Get(dataParameterDTO.Id);

            dataParameter.ControlObject = new ControlObject { Id = dataParameterDTO.ControlObject.Id };
            dataParameter.MeteringType = dataParameterDTO.MeteringType;
            dataParameter.Name = dataParameterDTO.Name;
            dataParameter.Value = dataParameterDTO.Value;
            dataParameter.LastUpdate = DateTime.Now;

            _fullDataParameterRepository.SaveOrUpdate(new FullDataParameter
                {
                    Id=0,
                    ControlObject = new ControlObject { Id = dataParameter.ControlObject.Id },
                    Name = dataParameter.Name,
                    Value = dataParameter.Value,
                    LastUpdate = DateTime.Now
                });

            return _dataParameterRepository.SaveOrUpdate(dataParameter);
        }

        public void SaveDataParameters(List<DataParameterDTO> dataParameterDTO, string guid)
        {

            try
            {
                var list = _dataParameterRepository.Query().Fetch(x => x.ControlObject).ToList().Where(m => m.ControlObject.Guid == guid).ToList();

                foreach (var dataParam in list)
                {
                    _dataParameterRepository.Delete(dataParam.Id);
                }

                foreach (var parameterDTO in dataParameterDTO)
                {
                    parameterDTO.ControlObject = new ControlObjectDTO { Id = list.First().ControlObject.Id };
                    //parameterDTO.ControlObject = new ControlObjectDTO { Id = controlObject.Id };
                    SaveOrUpdate(parameterDTO);
                }
            }
            catch (Exception)
            { }
        }

        public void Delete(int id)
        {
            _dataParameterRepository.Delete(id);
        }

        public List<DataParameter> GetDataParameterByControlObjectGuid(string guid)
        {
            return _dataParameterRepository.Query().Where(m => m.ControlObject.Guid == guid).ToList();
        }

        public List<FullDataParameter> GetFullDataParameterListByName(string name)
        {
            return _fullDataParameterRepository.Query().Where(m => m.Name == name).ToList();
        }

        public void ClineFullDataParameterByName(int controlObjectId, string name)
        {
            _fullDataParameterRepository.Query().Where(m => m.ControlObject.Id == controlObjectId && m.Name == name).ToList()
                .ForEach(fullDataParameter=> _fullDataParameterRepository.Delete(fullDataParameter.Id));
        }
    }
}
