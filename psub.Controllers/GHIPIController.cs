using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Psub.DTO.Entities;
using Psub.DataService.Abstract;
using Psub.Shared;
using Psub.Shared.Abstract;

namespace Psub.Controllers
{
    public class GHIPIController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public GHIPIController(IFileService fileService,
            IUserService userService)
        {
            _fileService = fileService;
            _userService = userService;
        }

        public ActionResult GHIPI()
        {
            if (_userService.GetCurrentUser().NickName.ToLower().Trim() == "gh" || _userService.GetCurrentUser().NickName.ToLower().Trim() == "ipi")
                return View(_fileService.GetDateList());
            return View(new List<string>());
        }

        public ActionResult GHIPIAlbum(string dateTime)
        {
            if (_userService.GetCurrentUser().NickName.ToLower().Trim() == "gh" || _userService.GetCurrentUser().NickName.ToLower().Trim() == "ipi")
                return View(_fileService.Files(Convert.ToDateTime(dateTime)));
            return View(new List<Domain.Entities.File>());
        }

        public FileResult File(string name, string dateTime)
        {
            if (_userService.GetCurrentUser().NickName.ToLower().Trim() == "GH" || _userService.GetCurrentUser().NickName.ToLower().Trim() == "ipi")
                return File(_fileService.File(name, dateTime), MimeTypes.GetMimeType(System.IO.Path.GetExtension(_fileService.File(name, dateTime))), HttpUtility.UrlPathEncode(Path.GetFileName(_fileService.File(name, dateTime))));
            return File("", "");
        }

        public ActionResult Test()
        {
            var r = new UserDTO();
            var dir_info = new DirectoryInfo(HttpContext.Server.MapPath("File/15.12.13"));
            r.Name = dir_info.Exists.ToString();
            foreach (FileInfo file in dir_info.GetFiles())
            {
                r.Name += dir_info.FullName + @"\" + file.Name + "     ";
            }
            return View(r);
        }
    }
}
