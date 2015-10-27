using psub.net.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace psub.net.Shared.Concrete
{
    public class FileService : IFileService
    {
        public IEnumerable<Psub.Domain.Entities.File> Files(DateTime dateTime)
        {
            var dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(string.Format("{0}/{1}", WebConfigurationManager.AppSettings["Files"], dateTime.ToShortDateString())));

            var files = dirInfo.GetFiles().Select(file => new Psub.Domain.Entities.File
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

        public bool SaveFile(HttpPostedFileBase file, string entityName, string guid, int id)
        {
            var extension = Path.GetExtension(file.FileName);
            var dirInfo = string.Empty;

            try
            {
                if (extension != null)
                {
                    var vFileName = string.Format("{0};{1}", id > 0 ? Guid.NewGuid().ToString() : guid, Path.GetFileName(file.FileName));

                    if (HttpContext.Current.Request.PhysicalApplicationPath != null)
                        dirInfo = string.Format("{0}\\{1}\\{2}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, ConfigData.FileDirectory, entityName), id, vFileName);

                    if (HttpContext.Current.Request.PhysicalApplicationPath != null
                        && !Directory.Exists(string.Format("{0}\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, ConfigData.FileDirectory, entityName), id)))
                    {
                        Directory.CreateDirectory(string.Format("{0}\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, ConfigData.FileDirectory, entityName), id));
                    }
                }

                file.SaveAs(dirInfo);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<Psub.Domain.Entities.File> GetFiles(string entityName, int id)
        {
            var result = new List<Psub.Domain.Entities.File>();
            var dir = string.Format("{0}\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, ConfigData.FileDirectory, entityName), id);

            if (Directory.Exists(dir))
                foreach (var file in new DirectoryInfo(dir).GetFiles())
                {
                    result.Add(new Psub.Domain.Entities.File
                    {
                        Name = file.Name,
                        Folder = file.DirectoryName
                    });
                }

            return result;
        }

        public string GetFile(string entityName, string name, int id)
        {
            return string.Format("{0}\\{1}\\{2}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, ConfigData.FileDirectory, entityName), id, name);
        }

        public List<Psub.Domain.Entities.File> GetFileList(string entityName, int id)
        {
            var dir = string.Empty;
            if (HttpContext.Current.Request.PhysicalApplicationPath != null)
                dir = string.Format("{0}\\{1}\\", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, entityName), id);
            if (System.IO.Directory.Exists(dir))
                return (from file in new DirectoryInfo(dir).GetFiles()
                        where file.Name.Split(';').Count() > 1
                        select new Psub.Domain.Entities.File
                        {
                            Name = file.Name.Split(';')[1],
                            Guid = file.Name.Replace(file.Extension, "").Split(';')[0]
                        }).ToList();
            return new List<Psub.Domain.Entities.File>();
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

        public string ImageUpload(HttpPostedFileBase upload)
        {
            var imagePath = String.Empty;
            var dirPath = string.Empty;
            var vFileName = string.Empty;
            var currentDate = DateTime.Now.ToShortDateString();
            var fileFolder = string.Format("{0}\\CatalogPublication\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath ?? string.Empty, "WebFiles"), DateTime.Now.ToShortDateString());

            if (upload != null && upload.ContentLength > 0)
            {
                var extension = Path.GetExtension(upload.FileName);
                var fileGuid = Guid.NewGuid();

                if (extension != null)
                {
                    vFileName = string.Format("{0}{1}", fileGuid, Path.GetFileName(upload.FileName));

                    dirPath = fileFolder + "\\" + vFileName;
                    if (!Directory.Exists(fileFolder))
                    {
                        Directory.CreateDirectory(fileFolder);
                    }
                }

                upload.SaveAs(dirPath);

                imagePath = VirtualPathUtility.ToAbsolute(string.Format("~/File/GetFilePath?date={0}&guid={1}", currentDate, fileGuid)); // string.Format("{0}/{1}", fileFolder.Replace(@"\", "/").Replace("//", @"\\\\"), vFileName);
            }
            return imagePath;
        }

        public string GetImagePath(string date, string guid)
        {
            var folder = string.Format("{0}\\CatalogPublication\\{1}", Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath ?? string.Empty, "WebFiles"), date);

            foreach (var file in new DirectoryInfo(folder).GetFiles(string.Format("*{0}*", guid)))
            {
                return file.FullName;
            }
            return string.Empty;
        }
    }
}
