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
    public class CatalogSectionController : Controller
    {
        private readonly ICatalogSectionService _publicationSectionService;
        private readonly ICatalogMainSectionService _publicationMainSectionService;

        public CatalogSectionController(ICatalogSectionService publicationSectionService,
        ICatalogMainSectionService publicationMainSectionService)
        {
            _publicationMainSectionService = publicationMainSectionService;
            _publicationSectionService = publicationSectionService;
        }

        public ActionResult List()
        {
            ViewData["CreatePublicationMainSection"] = _publicationMainSectionService.GetCatalogSectionDTOList();
            return View();
        }

        [HttpPost]
        public JsonResult GetCatalogMainSectionList()
        {
            return Json(_publicationMainSectionService.GetCatalogSectionDTOList());
        }
        [HttpPost]
        public JsonResult GetCatalogSectionList()
        {
            var publicationSectionDTOList = _publicationSectionService.GetCatalogSectionDTOList().ToList();
            publicationSectionDTOList.ForEach(m => m.Name = string.Format("{0} ({1})", m.Name, m.PublicationMainSection.Name));
            return Json(publicationSectionDTOList);
        }

        [HttpPost]
        public JsonResult CreateCatalogSection(string name, int catalogMainSectionId)
        {
            return Json(_publicationSectionService.SaveOrUpdate(new PublicationSectionDTO
                {
                    Id = 0,
                    Name = name,
                    PublicationMainSection = new PublicationMainSectionDTO { Id = catalogMainSectionId }
                }));
        }

        [HttpPost]
        public JsonResult CreateCatalogMainSection(string name)
        {
            return Json(_publicationMainSectionService.SaveOrUpdate(new PublicationMainSectionDTO
            {
                Id = 0,
                Name = name
            }));
        }

        [HttpPost]
        public JsonResult GetCatalogSectionDTOList()
        {
            return Json(_publicationMainSectionService.GetCatalogSectionDTOList());
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult DeleteCatalogMainSection(int id)
        {
            _publicationMainSectionService.Delete(id);
            return Json(true);
        }
        
        [HttpPost]
        [TransactionPerRequest]
        public JsonResult DeleteCatalogSection(int id)
        {
            _publicationSectionService.Delete(id);
            return Json(true);
        }

        [HttpPost]
        public JsonResult GetCatalogSectionDTOListByMainId(int id)
        {
            return Json(_publicationSectionService.GetCatalogSectionDTOList().Where(m => m.PublicationMainSection.Id == id));
        }
    }
}
