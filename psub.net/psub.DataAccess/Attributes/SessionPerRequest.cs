using System.Web.Mvc;

namespace Psub.DataAccess.Attributes
{
    public class SessionPerRequest : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // use this method instead of OnResultExecuted if the session does not
            // need to be open for the view rendering.
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            NHibernateHelper.DisposeSession();
        }

    }
}