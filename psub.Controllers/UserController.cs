using System;
using System.IO;
using System.Web.Mvc;
using Psub.DTO.Entities;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.Shared;

namespace Psub.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDrawingService _drawingService;

        public UserController(IUserService userService,
            IDrawingService drawingService)
        {
            _userService = userService;
            _drawingService = drawingService;
        }

        public ActionResult PrivateRoom()
        {
            return View(_userService.GetCurrentUser());
        }

        public ActionResult EditUserData()
        {
            return View(_userService.GetCurrentUser());
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult EditUserData(UserDTO user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.ConfirmPassword) && user.Password != user.ConfirmPassword)
                {
                    TempData["Message"] = "Данные не сохранены. Повнимательнее с изменением пароля!!!";
                    return View("PrivateRoom", user);
                }

                _userService.SaveOrUpdate(user);
                TempData["Message"] = "Данные сохранены";
            }
            catch (Exception)
            {
                TempData["Message"] = "Ошибка сохранения данных";
            }

            return View("PrivateRoom", user);
        }

        [HttpPost]
        public JsonResult GetUsers()
        {
            return Json(_userService.GetUsersDTO());
        }

        public ActionResult GetFile(int id, string name)
        {
            var path = _drawingService.GetFile(id, name);
            return File(path, MimeTypes.GetMimeType(Path.GetExtension(path)), Path.GetFileName(path));
        }
        
        [HttpPost]
        public JsonResult GetFilePath(int id, string name)
        {
            var path = _drawingService.GetFile(id, name);
            return Json("/"+path.Substring(path.IndexOf("User"), path.Length - path.IndexOf("User")));
        }
    }
}
