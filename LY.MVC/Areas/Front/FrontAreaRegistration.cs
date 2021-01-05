using System.Web.Mvc;

namespace LY.MVC.Areas.Front
{
    public class FrontAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Front";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Front_default",
                "Front/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "LY.MVC.Areas.Front.Controllers" }
            );
        }
    }
}