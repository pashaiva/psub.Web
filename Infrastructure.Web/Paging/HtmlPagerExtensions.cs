using System.Web;
using System.Web.Mvc;
using Infrastructure.Web.Extensions;

namespace Infrastructure.Web.Paging
{
    public static class HtmlPagerExtensions
    {
        private const string PagingViewName = "_Pager";

        public static HtmlString Pager(this HtmlHelper helper, int itemsCount)
        {
            var model = new ListPager(itemsCount);
            return new HtmlString(helper.ViewContext.RenderPartialViewToString(PagingViewName, model));
        }
        public static HtmlString Pager(this HtmlHelper helper, int itemsCount, int pageSize)
        {
            var model = new ListPager(itemsCount, pageSize);
            return new HtmlString(helper.ViewContext.RenderPartialViewToString(PagingViewName, model));
        }
        public static HtmlString Pager(this HtmlHelper helper, int itemsCount, int pageSize, int maxDisplayedPages)
        {
            var model = new ListPager(itemsCount, pageSize, maxDisplayedPages);
            return new HtmlString(helper.ViewContext.RenderPartialViewToString(PagingViewName, model));
        }
        public static HtmlString Pager(this HtmlHelper helper, int itemsCount, int pageSize, int maxDisplayedPages, string queryParameterName)
        {
            var model = new ListPager(itemsCount, pageSize, maxDisplayedPages, queryParameterName);
            return new HtmlString(helper.ViewContext.RenderPartialViewToString(PagingViewName, model));
        }
    }
}