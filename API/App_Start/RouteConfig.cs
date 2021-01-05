using LY.Web.Core.PipeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string date = DateTime.Now.ToString("yyyyMMdd");
            routes.IgnoreRoute(date + "Custom/{*path}");//路由配置 只要地址用的Custom 开头的都忽略


            routes.Add(date + "Custom", new CustomRoute());//拒绝浏览器、IP 、检查referer



            routes.Add(date + "LYRoute", new Route(date + "LYRoute", new LYRouteHandler()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
