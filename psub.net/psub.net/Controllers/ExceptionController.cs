using System.Web.Mvc;

namespace Psub.Controllers
{
  public  class ExceptionController:Controller
    {
      public ActionResult AccessIsClosed()
      {
          return View();
      }
      
      public ActionResult Error()
      {
          return View();
      }
    }
}
