using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Psub.DTO.Entities;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;

namespace Psub.Controllers
{
    public class PublicationSectionController : Controller
    {
        private readonly IPublicationSectionService _publicationSectionService;
        private readonly IPublicationMainSectionService _publicationMainSectionService;

        public PublicationSectionController(IPublicationSectionService publicationSectionService,
        IPublicationMainSectionService publicationMainSectionService)
        {
            _publicationMainSectionService = publicationMainSectionService;
            _publicationSectionService = publicationSectionService;
        }

        public ActionResult List()
        {
            ViewData["CreatePublicationMainSection"] = _publicationMainSectionService.GetPublicationSectionDTOList();
            return View();
        }

        [HttpPost]
        public JsonResult GetPublicationMainSectionList()
        {
            return Json(_publicationMainSectionService.GetPublicationSectionDTOList());
        }
        [HttpPost]
        public JsonResult GetPublicationSectionList()
        {
            var publicationSectionDTOList = _publicationSectionService.GetPublicationSectionDTOList().ToList();
            publicationSectionDTOList.ForEach(m => m.Name = string.Format("{0} ({1})", m.Name, m.PublicationMainSection.Name));
            return Json(publicationSectionDTOList);
        }

        [HttpPost]
        public JsonResult CreatePublicationSection(string name, int publicationMainSectionId)
        {
            return Json(_publicationSectionService.SaveOrUpdate(new PublicationSectionDTO
                {
                    Id = 0,
                    Name = name,
                    PublicationMainSection = new PublicationMainSectionDTO { Id = publicationMainSectionId }
                }));
        }

        [HttpPost]
        public JsonResult CreatePublicationMainSection(string name)
        {
            return Json(_publicationMainSectionService.SaveOrUpdate(new PublicationMainSectionDTO
            {
                Id = 0,
                Name = name
            }));
        }

        [HttpPost]
        public JsonResult GetPublicationSectionDTOList()
        {
            return Json(_publicationMainSectionService.GetPublicationSectionDTOList());
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult DeletePublicationMainSection(int id)
        {
            _publicationMainSectionService.Delete(id);
            return Json(true);
        }
        
        [HttpPost]
        [TransactionPerRequest]
        public JsonResult DeletePublicationSection(int id)
        {
            _publicationSectionService.Delete(id);
            return Json(true);
        }

        [HttpPost]
        public JsonResult GetPublicationSectionDTOListByMainId(int id)
        {
            return Json(_publicationSectionService.GetPublicationSectionDTOList().Where(m=>m.PublicationMainSection.Id==id));
        }
    }
}
