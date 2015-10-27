using System;
using System.IO;
using System.Web.Mvc;

namespace Infrastructure.Web.Extensions
{
    public static class ViewContextExtensions
    {
        public static string RenderPartialViewToString(this ViewContext viewContext, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                throw new ArgumentException("viewName");
            var context = new ControllerContext(viewContext.RequestContext, viewContext.Controller);
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var newViewContext = new ViewContext(context, viewResult.View, viewContext.ViewData, viewContext.TempData, sw)
                {
                    ViewData =
                    {
                        Model = model
                    }
                };
                viewResult.View.Render(newViewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
