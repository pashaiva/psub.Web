using System.Web;
using System.Web.Optimization;

namespace psub.net
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryold").Include(
                        "~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
              
            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                     "~/Scripts/ejs.js",
                     "~/Scripts/Custom/CommentsBuilder.js",
                     "~/Scripts/Custom/customModalDialog.js",
                     "~/Scripts/CKEditor/ckeditor.js",
                     "~/Scripts/CKEditor/config.js",
                     "~/Scripts/Custom/actionLogList.js",
                     "~/Scripts/Custom/CommentsBuilder.js",
                     "~/Scripts/Custom/CommentsBuilder.js"));
                       
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/commentStyles.css"));

            bundles.Add(new StyleBundle("~/Content/FancyBox2.1").Include(
                    "~/Scripts/FancyBox2.1/jquery.fancybox.css"));

            bundles.Add(new ScriptBundle("~/Scripts/FancyBox2.1").Include(
                  "~/Scripts/FancyBox2.1/jquery.fancybox.pack.js",
                  "~/Scripts/FancyBox2.1/jquery.fancybox.js"));
        }
    }
}
