using System.Web.Mvc;
using Psub.DataAccess.Attributes;
using Psub.DataService.HandlerPerQuery;
using Psub.DataService.HandlerPerQuery.LikeProcess.Entities;

namespace Psub.Controllers
{
    public class LikeController : BaseController
    {
        private readonly IMediator _mediator;

        public LikeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [TransactionPerRequest]
        public JsonResult SetLike(LikeCreateQuery query)
        {
            return Json(_mediator.RequestMvc(query));
        }
    }
}
