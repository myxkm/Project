using LY.Framework.Config;
using LY.Framework.Encrypt;
using LY.Game.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LY.Game.Areas.Admin.Controllers
{
    [Description("系统管理")]
    public class MainController : Controller
    {
        // GET: Admin/Main
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            Session["LOGIN_STAFF"] = null;
            LoginPostViewModel model = new LoginPostViewModel();
            if (Session["TRY_LOGIN_COUNT"] != null)
            {
                int count = Convert.ToInt32(Session["TRY_LOGIN_COUNT"]);
                if (count > 5)
                {
                    model.IsVerfiyCode = true;
                }
            }
            return View(model);
         
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginPostViewModel model)
        {
            if (Session["TRY_LOGIN_COUNT"] == null) Session["TRY_LOGIN_COUNT"] = 1;
            else
            {
                int count = Convert.ToInt32(Session["TRY_LOGIN_COUNT"]);
                if (count > 5)
                {
                    model.IsVerfiyCode = true;
                    //判断验证码是否正确
                }
                Session["TRY_LOGIN_COUNT"] = count + 1;
            }
            string password = string.Format("{0}{1}", StaticConstant.EncryptKey, model.Password);
            model.Password = MD5Encrypt.Encrypt(password);
            
            return View();
        }


        public ActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgot(string userName)
        {
            return View();
        }
        public ActionResult Reset()
        {
            return View();
        }

        [Description("退出")]
        public ActionResult Logout()
        {
            return RedirectToAction("login");
        }
    }
}