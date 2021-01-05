using SQLSugar_X.Sql.SqlSugar_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQLSugar_X.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            new DemoManager().SearchDemo();
            new DemoManager().InsertDemo();
            new DemoManager().DeleteDemo();
            new DemoManager().UpdateDemo();
            new DemoManager().TranDemo(() =>
            {

            });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}