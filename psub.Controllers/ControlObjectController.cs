using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Psub.DTO.Entities;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.Controllers
{
    public class ControlObjectController : Controller
    {
        private readonly IUserService _userService;
        private readonly IControlObjectService _controlObjectService;
        private readonly IDataParameterService _dataParameterService;
        private readonly IRelayDataService _relayDataService;
        private readonly IActionLogService _actionLogService;

        public ControlObjectController(IUserService userService,
            IControlObjectService controlObjectService,
            IDataParameterService dataParameterService,
            IRelayDataService relayDataService,
            IActionLogService actionLogService)
        {
            _userService = userService;
            _controlObjectService = controlObjectService;
            _dataParameterService = dataParameterService;
            _relayDataService = relayDataService;
            _actionLogService = actionLogService;
        }

        public ActionResult List()
        {
            if (Request.IsAuthenticated)
            {
                return View(_controlObjectService.GetControlObjectList(_userService.GetCurrentUser()));
            }
            return RedirectToAction("AccessIsClosed", "Exception");
        }
        public ActionResult Details(int id)
        {
            if (Request.IsAuthenticated)
            {
                return View(_controlObjectService.GetControlObjectById(id));
            }
            return RedirectToAction("AccessIsClosed", "Exception");
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult SetObjectsControl(string guid, string name, string val)
        {

            var dataParemeters = new List<DataParameterDTO>();
            var index = 0;
            var name1 = name.Split('|');
            var val1 = val.Split('|');
            foreach (var dataParameter in name1)
            {
                dataParemeters.Add(new DataParameterDTO
                    {
                        Name = dataParameter,
                        Value = val1[index]
                    });
                index++;
            }

            _dataParameterService.SaveDataParameters(dataParemeters, guid);

            var pinsString = "";
            var valString = "";
            foreach (var relayData in _relayDataService.GetRelayDataDTOByControlObjectGuid(guid))
            {
                pinsString = string.Format("{0}{1}{2}", pinsString, !string.IsNullOrEmpty(pinsString)
                    ? "|"
                    : "", relayData.PinName);
                valString = string.Format("{0}{1}{2}", valString, !string.IsNullOrEmpty(valString)
                    ? "|"
                    : "", relayData.Value ? 1 : 0);
            }

            return Json(string.Format("{0}*{1}", pinsString, valString));
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult Save(string name, string discription, int id)
        {
            try
            {
                _controlObjectService.SaveOrUpdate(new ControlObjectDTO
                           {
                               Id = id,
                               Name = name,
                               Discription = discription
                           });
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult SaveRelays(string relayData, int controlObjectId)
        {
            var releys = relayData.Split('*');
            var relayList = (from reley in releys
                             where !string.IsNullOrEmpty(reley)
                             select reley.Split('|')
                                 into stat
                                 select new RelayDataDTO
                                     {
                                         Id = Convert.ToInt32(stat[0]),
                                         Value = stat[1] == "true"
                                     }).ToList();

            _relayDataService.SaveOrUpdateList(relayList);

            return Json(true);
        }

        public JsonResult DataParameters(int id)
        {
            return Json(_dataParameterService.GetDataParameterDTOListByControlObjectId(id));
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult GetActionLogList(int id)
        {
            var actionLog = _actionLogService.GetActionLogDTOList(typeof(ControlObject).Name, id).ToList();
            var relays = _relayDataService.GetRelayDataDTOByControlObjectId(id);
            foreach (var relaysDTO in relays)
            {
                actionLog.AddRange(_actionLogService.GetActionLogDTOList(typeof(RelayData).Name, relaysDTO.Id).ToList());
            }

            return Json(actionLog.OrderBy(m => m.Date));
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult ClineDateParameter(int id, string name)
        {
            _dataParameterService.ClineFullDataParameterByName(id, name);
            return Json(true);
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult SetPin(string guid, string name, string val)
        {
            _relayDataService.UpdateBySMS(guid, name, val);

            return Json(true);
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult GetParametres()
        {
            var par = _dataParameterService.GetFullDataParameterListByName("Temperature, C");
            if (par.Count > 1000)
            {
                par = par.Skip(par.Count - 1000).Take(1000).ToList();
            }
            return Json(new { sec = par.Select(m => m.LastUpdate.Second), minite = par.Select(m => m.LastUpdate.Minute), hour = par.Select(m => m.LastUpdate.Hour), day = par.Select(m => m.LastUpdate.Day), month = par.Select(m => m.LastUpdate.Month), year = par.Select(m => m.LastUpdate.Year), val = par.Select(m => m.Value) });
        }
    }
}
