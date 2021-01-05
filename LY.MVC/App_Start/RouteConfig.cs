using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LY.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "AboutContents",
                url: "Contents",
                defaults: new { controller = "About", action = "Contents", id = UrlParameter.Optional },
                namespaces: new string[] { "LY.MVC.Controllers" }
                );


            routes.MapRoute(
             name: "AboutA",
             url: "A/{action}/{id}",
             defaults: new { controller = "About", action = "About", id = UrlParameter.Optional },
             namespaces: new string[] { "LY.MVC.Controllers" }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "LY.MVC.Controllers" }
            );
        }
    }
}
