using System;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Messages;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.DataService.HandlerPerQuery;
using Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Entities;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using psub.net.Shared;

namespace Psub.Controllers
{
    public class PublicationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IActionLogService _actionLogService;
        private readonly IPublicationCommentService _publicationCommentService;
        private readonly IUserService _userService;

        public PublicationController(IMediator mediator,
            IActionLogService actionLogService,
            IPublicationCommentService publicationCommentService,
            IUserService userService)
        {
            _mediator = mediator;
            _actionLogService = actionLogService;
            _publicationCommentService = publicationCommentService;
            _userService = userService;
        }

        public ActionResult Create(PublicationCreateGetQuery query)
        {
            if (_userService.IsAdmin())
                return View(_mediator.RequestMvc(query));

            AddNoAccessMessage();
            return RedirectToAction("List");
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Create(PublicationCreateQuery query)
        {
            return RedirectToAction("Details", new { id = _mediator.RequestMvc(query).Id });
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Edit(PublicationEditPostQuery query)
        {
            return RedirectToAction("Details", new { id = _mediator.RequestMvc(query).Id });
        }

        [TransactionPerRequest]
        public ActionResult Details(PublicationDetailsQuery query)
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
        public ActionResult Edit(PublicationEditGetQuery query)
        {
            if (_userService.IsAdmin())
                return View(_mediator.RequestMvc(query));

            AddNoAccessMessage();
            return RedirectToAction("List");
        }

        public ActionResult List(PublicationListQuery query)
        {
            return View(_mediator.RequestMvc(query));
        }
        
        [HttpPost]
        public JsonResult GetActionLogList(int id)
        {
            return Json(_actionLogService.GetActionLogDTOList(typeof(Publication).Name, id));
        }

        //[HttpPost]
        //[TransactionPerRequest]
        //public JsonResult SaveComment(int objectId, string text, int commentId)
        //{
        //    text = HttpUtility.HtmlDecode(text);
        //    return Json(_publicationCommentService.Save(objectId, text, commentId));
        //}

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult SaveComment(string objectId, string text, string parentId)
        {
            text = HttpUtility.HtmlDecode(text);
            return Json(_publicationCommentService.Save(Convert.ToInt32(objectId), text, parentId != null ? Convert.ToInt32(parentId) : 0));
        }

        [HttpPost]
        public JsonResult GetComments(PublicationCommentListQuery query)
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
