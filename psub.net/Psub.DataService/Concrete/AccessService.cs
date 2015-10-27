using System.Web.Mvc;
using Psub.DataService.Abstract;

namespace Psub.DataService.Concrete
{
    public class AccessService : ActionFilterAttribute
    {
       

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.HttpContext.Session != null 
                && filterContext.HttpContext.Session["isAdmin"]!=null
                && (bool)filterContext.HttpContext.Session["isAdmin"]))
                filterContext.Result = new RedirectResult("~/Exception/AccessIsClosed");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // use this method instead of OnResultExecuted if the transaction / session 
            // does not need be open for the view rendering.
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
    }
}
