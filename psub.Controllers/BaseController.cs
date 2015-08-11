using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Psub.Shared;

namespace Psub.Controllers
{
    public class BaseController : Controller
    {
        private const string MessageKey = "Message";

        protected void AddMessage(string text, Infrastructure.Messages.MessageTypes type)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                List<Infrastructure.Messages.Message> messages;
                if (TempData[MessageKey] == null)
                {
                    messages = new List<Infrastructure.Messages.Message>();
                }
                else
                {
                    messages = (List<Infrastructure.Messages.Message>)TempData[MessageKey];
                }
                messages.Add(new Infrastructure.Messages.Message(text, type));
                TempData[MessageKey] = messages;
            }
        }

        protected void AddNoAccessMessage()
        {
            AddMessage("Этот документ вам недоступен.", Infrastructure.Messages.MessageTypes.Warning);
        }

        protected FileResult ReturnFile(string path, string fileName = "", bool isLoadOnly = false)
        {
            var extension = Path.GetExtension(path);
            if (extension != null && ConfigData.FileExtensionForOpenInBrowser.Contains(extension.ToLower()) && !isLoadOnly)
                return File(path, MimeTypes.GetMimeType(extension));
            return File(path, MimeTypes.GetMimeType(extension), string.IsNullOrEmpty(fileName) ? Path.GetFileName(path) : fileName);
        }
    }
}
