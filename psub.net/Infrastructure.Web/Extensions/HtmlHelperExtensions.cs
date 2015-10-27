using System;
using System.Web;
using System.Web.Mvc;

namespace Infrastructure.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString RenderResponseDuration(this HtmlHelper helper)
        {
            return new HtmlString(string.Format("{0} ms", (DateTime.Now - helper.ViewContext.HttpContext.Timestamp).TotalMilliseconds));
        }

        public static HtmlString RenderBoolValue(this HtmlHelper helper, bool value)
        {
            return new HtmlString(value ? "да" : string.Empty);
        }
    }
}
