using LY.Framework.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LY.MVC.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string account, string pwd, string verifycode)
        {
            var currentUrl = HttpContext.Session[StaticConstant.CurrentUrlSession];
            if (currentUrl != null && string.IsNullOrEmpty(currentUrl.ToString()))
            {
                HttpContext.Session.Remove(StaticConstant.CurrentUrlSession);
                return base.Redirect(currentUrl.ToString());
            }
            return base.RedirectToAction("index", "Home");
        }
    }
}