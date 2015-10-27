using System.Web.Mvc;

namespace Psub.DataAccess.Attributes
{
    public class TransactionPerRequest : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            NHibernateHelper.GetCurrentSession().BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // use this method instead of OnResultExecuted if the transaction / session 
            // does not need be open for the view rendering.
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
                NHibernateHelper.GetCurrentSession().Transaction.Commit();
            else
                NHibernateHelper.GetCurrentSession().Transaction.Rollback();
            NHibernateHelper.DisposeSession();
        }
    }
}