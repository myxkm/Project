using LY.Web.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //依赖注入
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
        }

        protected void GlobalHttpModule_LY(object sender, EventArgs e) {
                

        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if (lastError != null)
            {
                HttpException httpError = lastError as HttpException;
                string message = string.Empty;
                if (httpError != null)
                {
                    //获取错误代码
                    int httpCode = httpError.GetHttpCode();
                    message = httpError.Message;
                    if (httpCode == 400 || httpCode == 404)
                    {
                        Response.StatusCode = 404;
                        Response.WriteFile("~/Views/Shared/Error.cshtml");
                        Server.ClearError();
                        return;
                    }
                }
                message = lastError.Message;
                Response.StatusCode = 500;
                Response.WriteFile("~/Views/Shared/Error.cshtml");
                //一定要调用Server.ClearError()否则会触发错误详情页（就是黄页）
                Server.ClearError();
                Server.Transfer("~/Views/Shared/Error.cshtml");
            }

        }
    }
}
