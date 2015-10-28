using Psub.DataService;
using System;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace psub.net
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperStartup.Configure();
        }

        void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            if (httpException != null)
            {
                //switch (httpException.GetHttpCode())
                //{
                //    case 404:
                //        HttpContext.Current.Session["Message"] = "Страница не найдена!";
                //        break;
                //    case 500:
                //        //action = "Error";
                //        break;
                //    default:
                //        // action = "Error";
                //        HttpContext.Current.Session["Message"] = "Неизвестная ошибка. Попробуйте повторить действие позже.";
                //        break;
                //}
            }
            else
                HttpContext.Current.Session["Message"] = exception.Message;

            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress("pashaiva@mail.ru"));
                message.Subject = "psub.net error";
                message.Body = exception.Message;
                message.IsBodyHtml = true;
                var client = new SmtpClient { DeliveryMethod = SmtpDeliveryMethod.Network };
                client.Send(message);
            }
            catch { }
                   
            Response.Redirect(@"~/Exception/Error");
        }
    }
}
