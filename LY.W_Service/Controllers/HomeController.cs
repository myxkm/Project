using LY.W_Service.ServiceWcf;
using LY.W_Service.WebService1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LY.W_Service.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            WebService1SoapClient   webService1SoapClient = new WebService1SoapClient();
            webService1SoapClient.insertCargoInfo(1, 2);

            SearchServiceClient searchServiceClient = new SearchServiceClient();
            searchServiceClient.adasd(1,2);
            searchServiceClient.Close();
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