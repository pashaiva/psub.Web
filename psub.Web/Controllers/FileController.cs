using System.IO;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Messages;
using Psub.Shared;
using Psub.Shared.Abstract;

namespace Psub.Controllers
{
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public string UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum)
        {
            return string.Format(
                "<html><body><script>window.parent.CKEDITOR.tools.callFunction({0}, \"{1}\");</script></body></html>",
                CKEditorFuncNum, _fileService.ImageUpload(upload));
        }

        public FileResult GetFilePath(string date, string guid)
        {
            return ReturnFile(_fileService.GetImagePath(date, guid));
        }

        [HttpPost]
        public bool SaveFile(HttpPostedFileBase file, int id, string guid, string type)
        {
            _fileService.SaveFile(file, type, guid, id);
            
            return false;// file.FileName.ToString();
        }


        public ActionResult GetFile(string entityName, string name, int id)
        {
            var filePath =_fileService.GetFile(entityName, name, id);

            return ReturnFile(filePath);
        }
    }
}
