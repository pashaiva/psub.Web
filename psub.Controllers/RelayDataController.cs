using System;
using System.Web.Mvc;
using Psub.DTO.Entities;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;

namespace Psub.Controllers
{
    public class RelayDataController : Controller
    {
        private readonly IRelayDataService _relayDataService;

        public RelayDataController(IRelayDataService relayDataService)
        {
            _relayDataService = relayDataService;
        }

        public ActionResult Create(int id)
        {
            if (Request.IsAuthenticated)
            {
                return View(new RelayDataDTO
                    {
                        ControlObject = new ControlObjectDTO { Id = id }
                    });
            }
            return RedirectToAction("AccessIsClosed", "Exception");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Create(RelayDataDTO relayDataDTO)
        {
            if (Request.IsAuthenticated)
            {
                relayDataDTO.Id = 0;
                _relayDataService.SaveOrUpdate(relayDataDTO);
                return RedirectToAction("Details", "ControlObject", new { id = relayDataDTO.ControlObject.Id });
            }
            return RedirectToAction("AccessIsClosed", "Exception");
        }

        public ActionResult Edit(int id)
        {
            if (Request.IsAuthenticated)
            {
                return View(_relayDataService.GetRelayDataDTOById(id));
            }
            return RedirectToAction("AccessIsClosed", "Exception");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Edit(RelayDataDTO relayDataDTO)
        {
            if (Request.IsAuthenticated)
            {
                _relayDataService.SaveOrUpdate(relayDataDTO);
                return RedirectToAction("Details", "ControlObject", new { id = _relayDataService.GetControlObjectIdByRelayDataId(relayDataDTO.Id) });
            }
            return RedirectToAction("AccessIsClosed", "Exception");
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult Delete(int id)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    _relayDataService.Delete(id);
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
            return Json(false);
        }
    }
}
