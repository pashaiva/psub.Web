using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Psub.DataService;
using psub.Web.App_Start;

namespace psub.Web
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
           // BootstrapSupport.BootstrapBundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
            AutoMapperStartup.Configure();
           // BootstrapMvcSample.ExampleLayoutsRouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        HttpContext.Current.Session["Message"] = "Страница не найдена!";
                        break;
                    case 500:
                        //action = "Error";
                        break;
                    default:
                        // action = "Error";
                        HttpContext.Current.Session["Message"] = "Неизвестная ошибка. Попробуйте повторить действие позже.";
                        break;
                }
            }
            else
                HttpContext.Current.Session["Message"] = exception.Message;

            Response.Redirect(@"~/Exception/Error");
        }

        void Session_start()
        {

        }
    }
}