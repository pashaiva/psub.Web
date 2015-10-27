using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Psub.DataService.DTO;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery;
using Psub.DataService.HandlerPerQuery.ActionLogProcess.Entities;
using psub.net.Shared;
using Psub.DataService.HandlerPerQuery.HomeProcess.Entities;

namespace psub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly IStatisticService _statisticService;
        private readonly IMediator _mediator;

        public HomeController(IPublicationService publicationService,
            IStatisticService statisticService,
            IMediator mediator)
        {
            _publicationService = publicationService;
            _statisticService = statisticService;
            _mediator = mediator;
        }

        public ActionResult Index(HomeIndexQuery query)
        {            
            return View(_mediator.RequestMvc(query));
        }

        [HttpPost]
        public ActionResult Index(PublicationDTO publication)
        {
           return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var vImagePath = String.Empty;
            var vMessage = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();
                    string vFolderPath = AppDomain.CurrentDomain.GetData("Files").ToString();

                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }

                    string vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);

                    vImagePath = Url.Content(vFolderPath + vFileName);
                    vMessage = "Image was saved correctly";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }

            string vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" +
                             vImagePath + "\", \"" + vMessage + "\");</script></body></html>";

            return Content(vOutput);
        }

        public ActionResult About()
        {
            ViewBag.Message = ConfigData.ApplicationName;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты";

            return View();
        }

        [HttpPost]
        JsonResult GetMenu()
        {
            return Json("");
        }

        [HttpPost]
        public JsonResult SetUserStatistic(string url, string urlReferrer)
        {
            _statisticService.Save(url, urlReferrer);
            return Json(true);
        }

        [HttpPost]
        public ActionResult GetLastActionsHistoryOfSectionList(LastActionsHistoryOfSectionListQuery model)
        {
            return Json(_mediator.RequestMvc(model));
        }
    }
}
