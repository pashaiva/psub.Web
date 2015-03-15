using System;
using System.Collections.Generic;
using System.Web;

namespace Psub.Shared.Abstract
{
    public interface IFileService
    {
        IEnumerable<Psub.Domain.Entities.File> Files(DateTime dateTime);
        string File(string name, string dateTime);
        List<string> GetDateList(string path = "File");
        string SaveFile(HttpPostedFileBase file, string entityName, string guid, int id);
        List<Domain.Entities.File> GetFileList(string entityName, int id);
        string GetFile(string entityName, int id, string guid);
        void DeleteFile(string entityName, int id, string guid);
        void CopyNewFile(string entityName, int id, string guid);
    }
}
