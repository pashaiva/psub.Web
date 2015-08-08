using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using File = Psub.Domain.Entities.File;

namespace Psub.Shared.Concrete
{
    public class FileService : Abstract.IFileService
    {
        public IEnumerable<File> Files(DateTime dateTime)
        {
            var dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(string.Format("{0}/{1}", WebConfigurationManager.AppSettings["Files"], dateTime.ToShortDateString())));

            var files = dirInfo.GetFiles().Select(file => new File
                {
                    Name = file.Name,
                    DateTime = dateTime
                }).ToList();

            return files;
        }

        public string File(string name, string dateTime)
        {
            return string.Format("http://psub.net/GHIPI/{0}/{1}/{2}", WebConfigurationManager.AppSettings["Files"],
                                       dateTime, name);
        }

        public List<string> GetDateList(string path = "File")
        {
            var dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));
            var list = dirInfo.GetDirectories().Select(file => file.Name).ToList();
            return list;
        }

        public string SaveFile(HttpPostedFileBase file, string entityName, string guid, int id)
        {
            var extension = Path.GetExtension(file.FileName);
            var dirInfo = string.Empty;

            if (extension != null)
            {
                var vFileName = string.Format("{0};{1}", id > 0 ? Guid.NewGuid().ToString() : guid, Path.GetFileName(file.FileName));

                if (HttpContext.Current.Request.PhysicalApplicationPath != null)
                    dirInfo = string.Format("{0}\\{1}\\{2}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id, vFileName);

                if (HttpContext.Current.Request.PhysicalApplicationPath != null
                    && !Directory.Exists(string.Format("{0}\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id)))
                {
                    Directory.CreateDirectory(string.Format("{0}\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id));
                }
            }

            file.SaveAs(dirInfo);

            return dirInfo;
        }

        public List<Domain.Entities.File> GetFileList(string entityName, int id)
        {
            var dir = string.Empty;
            if (HttpContext.Current.Request.PhysicalApplicationPath != null)
                dir = string.Format("{0}\\{1}\\", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id);
            if (System.IO.Directory.Exists(dir))
                return (from file in new DirectoryInfo(dir).GetFiles()
                        where file.Name.Split(';').Count() > 1
                        select new File
                            {
                                Name = file.Name.Split(';')[1],
                                Guid = file.Name.Replace(file.Extension, "").Split(';')[0]
                            }).ToList();
            return new List<File>();
        }

        public string GetFile(string entityName, int id, string guid)
        {
            if (HttpContext.Current.Request.PhysicalApplicationPath != null && Directory.Exists(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName) + @"\"))
            {
                var dirPath = string.Format("{0}\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id);
                var filePath = new DirectoryInfo(dirPath).GetFiles().FirstOrDefault(m => m.Name.Contains(guid));
                if (filePath != null && System.IO.File.Exists(string.Format("{0}\\{1}", filePath.DirectoryName, filePath.Name)))
                    return string.Format("{0}\\{1}", filePath.DirectoryName, filePath.Name);
            }
            return string.Empty;
        }

        public void DeleteFile(string entityName, int id, string guid)
        {
            if (HttpContext.Current.Request.PhysicalApplicationPath != null && Directory.Exists(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName) + @"\"))
            {
                var dirPath = string.Format("{0}\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id);

                var filePath = new DirectoryInfo(dirPath).GetFiles().FirstOrDefault(m => m.Name.Contains(guid));
                if (filePath != null && System.IO.File.Exists(string.Format("{0}\\{1}", filePath.DirectoryName, filePath.Name)))
                    if (filePath.DirectoryName != null) System.IO.File.Delete(string.Format("{0}\\{1}", filePath.DirectoryName, filePath.Name));
            }
        }

        public void CopyNewFile(string entityName, int id, string guid)
        {
            var dir = string.Empty;
            var newDir = string.Empty;
            if (HttpContext.Current.Request.PhysicalApplicationPath != null)
            {
                dir = string.Format("{0}\\0", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName));
                newDir = string.Format("{0}\\{1}\\", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id);
            }
            if (System.IO.Directory.Exists(dir))
                foreach (var file in new DirectoryInfo(dir).GetFiles().Where(m => m.Name.Contains(guid)))
                {
                    if (file.DirectoryName != null)
                        System.IO.File.Move(string.Format("{0}\\{1}", file.DirectoryName, file.Name), newDir + file.Name);
                }
        }
    }
}
