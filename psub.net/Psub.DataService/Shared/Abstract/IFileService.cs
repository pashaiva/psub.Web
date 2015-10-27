using System;
using System.Collections.Generic;
using System.Web;
using Psub.Domain.Entities;

namespace psub.net.Shared.Abstract
{
    public interface IFileService
    {
        IEnumerable<File> Files(DateTime dateTime);
        string File(string name, string dateTime);
        List<string> GetDateList(string path = "File");
        bool SaveFile(HttpPostedFileBase file, string entityName, string guid, int id);
        List<File> GetFileList(string entityName, int id);
        string GetFile(string entityName, int id, string guid);
        void DeleteFile(string entityName, int id, string guid);
        void CopyNewFile(string entityName, int id, string guid);
        string ImageUpload(HttpPostedFileBase upload);
        string GetImagePath(string date, string guid);
        List<File> GetFiles(string entityName, int id);
        string GetFile(string entityName, string name, int id);
    }
}
