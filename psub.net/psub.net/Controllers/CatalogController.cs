using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Messages;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.DataService.HandlerPerQuery;
using Psub.DataService.HandlerPerQuery.CatalogCommentProcess.Entities;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities;
using Psub.Domain.Entities;
using psub.net.Shared;
using psub.net.Shared.Abstract;

namespace Psub.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IActionLogService _actionLogService;
        private readonly ICatalogCommentService _catalogCommentService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public CatalogController(IMediator mediator,
            IActionLogService actionLogService,
            ICatalogCommentService catalogCommentService,
            IUserService userService,
            IFileService fileService)
        {
            _mediator = mediator;
            _actionLogService = actionLogService;
            _catalogCommentService = catalogCommentService;
            _userService = userService;
            _fileService = fileService;
        }

        public ActionResult Create(CatalogCreateGetQuery query)
        {
            if (_userService.IsAdmin())
                return View(_mediator.RequestMvc(query));

            AddNoAccessMessage();
            return RedirectToAction("List");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Create(CatalogCreateQuery query)
        {
            var result = _mediator.RequestMvc(query);

            var filesName = string.Empty;
            if (Request.Files.Count > 0)
            //парсим httpPostedFile (при отсутствии html5,flash,silverlight,browserplus,gears)
            {
                for (int fileIndex = 0; fileIndex < Request.Files.Count; fileIndex++)
                {
                    var httpPostedFileBase = Request.Files[fileIndex];

                    if (httpPostedFileBase != null &&
                        httpPostedFileBase.ContentLength < ConfigData.FileMaxSize * 1024 * 1024 &&
                        httpPostedFileBase.ContentLength > 0)
                    {
                        if (!_fileService.SaveFile(httpPostedFileBase, typeof(Catalog).Name, query.Guid, result.Id))
                            filesName = string.Format("{0}{1}; ", filesName, Path.GetFileName(httpPostedFileBase.FileName));
                    }
                    else
                    {
                        if (httpPostedFileBase != null)
                            filesName = string.Format("{0}{1}; ", filesName, Path.GetFileName(httpPostedFileBase.FileName));
                    }
                }

                if (filesName != "")
                    filesName = string.Format("Следующие файлы не были загружены:{0} <p class='redText'>Размер файлов более {1} Мб или недопустимое расширение!</p>",
                            filesName, ConfigData.FileMaxSize);
            }

            AddMessage(filesName, MessageTypes.Info);

            return RedirectToAction("Details", new { Id = result.Id });
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Edit(CatalogEditPostQuery query)
        {
            return RedirectToAction("Details", new { id = _mediator.RequestMvc(query).Id });
        }

        [TransactionPerRequest]
        public ActionResult Details(CatalogDetailsQuery query)
        {
            var result = _mediator.RequestMvc(query);
            if (result == null)
            {
                AddNoAccessMessage();
                return RedirectToAction("List");
            }
            return View(result);
        }

        [AccessService]
        public ActionResult Edit(CatalogEditGetQuery query)
        {
            if (_userService.IsAdmin())
                return View(_mediator.RequestMvc(query));

            AddNoAccessMessage();
            return RedirectToAction("List");
        }

        public ActionResult List(CatalogListQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }
        
        [HttpPost]
        public JsonResult GetActionLogList(int id)
        {
            return Json(_actionLogService.GetActionLogDTOList(typeof(Catalog).Name, id));
        }

        //[HttpPost]
        //[TransactionPerRequest]
        //public JsonResult SaveComment(int objectId, string text, int commentId)
        //{
        //    text = HttpUtility.HtmlDecode(text);
        //    return Json(_CatalogCommentService.Save(objectId, text, commentId));
        //}

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult SaveComment(string objectId, string text, string parentId)
        {
            text = HttpUtility.HtmlDecode(text);
            return Json(_catalogCommentService.Save(Convert.ToInt32(objectId), text, parentId != null ? Convert.ToInt32(parentId) : 0));
        }

        [HttpPost]
        public JsonResult GetComments(CatalogCommentListQuery query)
        {
            return Json(_mediator.RequestMvc(query).Items);
        }

        [HttpPost]
        public JsonResult GetMainSection(CatalogMainSectionListQuery query)
        {
            return Json(_mediator.RequestMvc(query));
        }

        public ActionResult CreateSection(CatalogSectionCreateGetQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }

        public ActionResult EditSection(CatalogSectionEditGetQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }

        [TransactionPerRequest]
        public ActionResult DeleteSection(CatalogSectionDeleteGetQuery query)
        {
            var result = _mediator.RequestMvc(query);
            if (!result.Result)
                AddMessage(string.Format("{0} {1}", LanguageConstants.FailedDeleteData, result.Message), MessageTypes.Danger);
            return RedirectToAction("List");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult EditSection(CatalogSectionEditPostQuery query)
        {
            if (_mediator.RequestMvc(query).Id > 0)
                AddMessage(LanguageConstants.SuccessSavedData, MessageTypes.Success);
            else
                AddMessage(LanguageConstants.FailedSaveData, MessageTypes.Danger);

            return RedirectToAction("List");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult CreateMainSection(CatalogMainSectionCreatePostQuery query)
        {
            if (_mediator.RequestMvc(query).Id > 0)
                AddMessage(LanguageConstants.SuccessSavedData, MessageTypes.Success);
            else
                AddMessage(LanguageConstants.FailedSaveData, MessageTypes.Danger);

            return RedirectToAction("List");
        }

        public ActionResult CreateMainSection(CatalogMainSectionCreateGetQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }
    }
}
