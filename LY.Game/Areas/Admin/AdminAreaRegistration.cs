using System.Web.Mvc;

namespace LY.Game.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "Admin_Main_Login",
               "Login",
               new { controller = "Main", action = "Login", id = UrlParameter.Optional }
           );
            context.MapRoute(
              "Admin_Main_Forgot",
              "Forgot",
              new { controller = "Main", action = "Forgot", id = UrlParameter.Optional }
          ); context.MapRoute(
              "Admin_Main_Reset",
              "Reset",
              new { controller = "Main", action = "Reset", id = UrlParameter.Optional }
          );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}