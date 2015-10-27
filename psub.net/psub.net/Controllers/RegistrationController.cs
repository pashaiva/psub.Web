using System.Web.Security;
using System.Web.Mvc;
using Psub.DataService.DTO;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;

namespace Psub.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Save(UserDTO user)
        {
            _userService.SetUserGuid(_userService.GetUserById(_userService.SaveOrUpdate(user)).UserGuid);
            FormsAuthentication.SetAuthCookie(user.Name, true);
            return RedirectToAction("Index", "Home");
        }
    }
}
