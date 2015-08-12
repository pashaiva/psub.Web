using System;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Messages;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.DataService.HandlerPerQuery;
using Psub.DataService.HandlerPerQuery.CatalogCommentProcess.Entities;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;

namespace Psub.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IActionLogService _actionLogService;
        private readonly ICatalogCommentService _catalogCommentService;
        private readonly IUserService _userService;

        public CatalogController(IMediator mediator,
            IActionLogService actionLogService,
            ICatalogCommentService catalogCommentService,
            IUserService userService)
        {
            _mediator = mediator;
            _actionLogService = actionLogService;
            _catalogCommentService = catalogCommentService;
            _userService = userService;
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
            return RedirectToAction("Details", new { id = _mediator.RequestMvc(query).Id });
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
        public JsonResult GetMainSection(MainSectionListQuery query)
        {
            return Json(_mediator.RequestMvc(query));
        }

        public ActionResult CreateSection(SectionCreateGetQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }

        public ActionResult EditSection(SectionEditGetQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }

        [TransactionPerRequest]
        public ActionResult DeleteSection(SectionDeleteGetQuery query)
        {
            var result = _mediator.RequestMvc(query);
            if (!result.Result)
                AddMessage(string.Format("{0} {1}", LanguageConstants.FailedDeleteData, result.Message), MessageTypes.Danger);
            return RedirectToAction("List");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult EditSection(SectionEditPostQuery query)
        {
            if (_mediator.RequestMvc(query).Id > 0)
                AddMessage(LanguageConstants.SuccessSavedData, MessageTypes.Success);
            else
                AddMessage(LanguageConstants.FailedSaveData, MessageTypes.Danger);

            return RedirectToAction("List");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult CreateMainSection(MainSectionCreatePostQuery query)
        {
            if (_mediator.RequestMvc(query).Id > 0)
                AddMessage(LanguageConstants.SuccessSavedData, MessageTypes.Success);
            else
                AddMessage(LanguageConstants.FailedSaveData, MessageTypes.Danger);

            return RedirectToAction("List");
        }

        public ActionResult CreateMainSection(MainSectionCreateGetQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }
    }
}
