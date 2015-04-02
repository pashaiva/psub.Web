using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Psub.DTO.Entities;
using Psub.DataAccess.Attributes;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.Domain.Entities;
using Psub.Shared;
using Psub.Shared.Abstract;

namespace Psub.Controllers
{
    public class PublicationController : Controller
    {
        private readonly IPublicationService _publicationService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private const string FileExtension = "pdf zip jpg gif png";

        public PublicationController(IPublicationService publicationService,
            IUserService userService,
            IFileService fileService)
        {
            _publicationService = publicationService;
            _userService = userService;
            _fileService = fileService;
        }

        [AccessService]
        public ActionResult Create()
        {
            return View(_publicationService.GetPublicationById(0));
        }

        [AccessService]
        public ActionResult Edit(int id)
        {
            return View(_publicationService.GetPublicationById(id));
        }

        public ActionResult Details(int id)
        {
            return View(_publicationService.GetPublicationById(id));
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult Create(PublicationDTO publication)
        {
            var filesName = "";
            var filesNameErrorExtension = "";
            if (Request.Files.Count > 0)//парсим httpPostedFile (при отсутствии html5,flash,silverlight,browserplus,gears)
            {
                for (int fileIndex = 0; fileIndex < Request.Files.Count; fileIndex++)
                {
                    var httpPostedFileBase = Request.Files[fileIndex];
                    var isValidExtension =
                        FileExtension.Contains(Path.GetExtension(httpPostedFileBase.FileName)
                                                   .ToLower()
                                                   .Replace(".", string.Empty));
                    if (httpPostedFileBase != null && httpPostedFileBase.ContentLength < 10 * 1024 * 1024 && httpPostedFileBase.ContentLength > 0
                        && isValidExtension)
                        _fileService.SaveFile(httpPostedFileBase, typeof(Publication).Name, publication.Guid, publication.Id);
                    else
                    {
                        if (httpPostedFileBase != null)
                        {

                            if (isValidExtension)
                            {
                                if (httpPostedFileBase.FileName.LastIndexOf('\\') != -1)
                                {//for IE
                                    filesName = string.Format("{0}{1}; ", filesName, httpPostedFileBase.FileName.Substring(httpPostedFileBase.FileName.LastIndexOf('\\') + 1, httpPostedFileBase.FileName.Length - 1 - httpPostedFileBase.FileName.LastIndexOf('\\')));
                                }
                                else
                                {//other browsers :)))
                                    filesName = string.Format("{0}{1}; ", filesName, httpPostedFileBase.FileName);
                                }
                            }
                            else
                                filesNameErrorExtension = string.Format("{0}{1}; </br>", filesNameErrorExtension, string.Format("{0}{1}; ", filesName, httpPostedFileBase.FileName));
                        }
                        else
                            filesNameErrorExtension = "Файлы отсутствуют!";
                    }
                }

                if (filesName != "")
                    filesName = string.Format("Следующие файлы не были загружены:{0} Размер файлов более 10 Мб!", filesName);
                if (!string.IsNullOrEmpty(filesNameErrorExtension))
                    System.Web.HttpContext.Current.Session["Message"] = string.Format("{0} </br> Файлы не были загружены из-за неверного типа: </br> {1}", filesName, filesNameErrorExtension);
            }

            var id = _publicationService.SaveOrUpdate(publication);



            return RedirectToAction("List");
        }

        public ActionResult List(int publicationSectionId = 0, int publicationMainSectionId = 0)
        {
            var list = _publicationService.GetPublicationList(publicationSectionId, publicationMainSectionId).ToList();
            return View(list);
        }

        [HttpPost]
        [TransactionPerRequest]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var vImagePath = String.Empty;
            var dirInfo = string.Empty;
            var vFileName = string.Empty;
            string fileFolder = string.Format("Publication" + "\\{0}", DateTime.Now.Date.ToShortDateString());

          if (Request.PhysicalApplicationPath != null && !Directory.Exists(Path.Combine(Request.PhysicalApplicationPath, fileFolder)))
              Directory.CreateDirectory(Server.MapPath(fileFolder));

            if (upload != null && upload.ContentLength > 0)
            {
                var extension = Path.GetExtension(upload.FileName);
                if (extension != null)
                {
                    vFileName = string.Format("{0};{1}", Guid.NewGuid(), Path.GetFileName(upload.FileName));

                    if (Request.PhysicalApplicationPath != null)
                    {
                        dirInfo = Path.Combine(Request.PhysicalApplicationPath, fileFolder) + "\\" + vFileName;
                        if (!Directory.Exists(Path.Combine(Request.PhysicalApplicationPath, fileFolder)))
                        {
                            Directory.CreateDirectory(Path.Combine(Request.PhysicalApplicationPath, fileFolder));
                        }
                    }
                }

                upload.SaveAs(dirInfo);

                vImagePath = string.Format("/{0}/{1}", fileFolder, vFileName);
            }

            var vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" +
                             vImagePath + "');</script></body></html>";

            return Content(vOutput);
        }

        [HttpPost]
        public bool File(HttpPostedFileBase file, int id, string guid)
        {
            if (string.IsNullOrEmpty(_fileService.SaveFile(file, typeof(Publication).Name, guid, id)))
            {
                //Файл не сохранен(ошибка во время сохранения)
            }
            return false;// file.FileName.ToString();
        }

        public ActionResult GetFile(string guid, int id)
        {
            string path = _fileService.GetFile(typeof(Publication).Name, id, guid);
            if (string.IsNullOrEmpty(path))
                return new HttpStatusCodeResult(404, "Файл не найден");
            return File(path, MimeTypes.GetMimeType(System.IO.Path.GetExtension(path)), Path.GetFileName(path));
        }

        [HttpPost]
        public JsonResult DeleteFile(int id, string guid)
        {
            try
            {
                _fileService.DeleteFile(typeof(Publication).Name, id, guid);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [TransactionPerRequest]
        public ActionResult DeletePublication(int id)
        {
            try
            {
                _publicationService.DeletePublication(id);
                return RedirectToAction("List");
            }
            catch (Exception)
            {
                return RedirectToAction("Details", "Publication", new { id = id });
            }
        }
    }
}
