using LY.Bussiness.Interface;
using LY.EF.Model;
using LY.Framework.ExtendExpression;
using LY.Remote.Interface;
using LY.Web.Core.Filter;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LY.MVC.Controllers
{
    
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController(ICategoryService categoryService, ISearchService searchService, ISearchService ssearchService) : base(categoryService, searchService)
        {
        }
        public ActionResult Index()
        {
            var modelset = _iCategoryService.Set<Caregory>();
            try
            {
                var a = modelset.Where(t => t.Id > 0);
            }
            catch (Exception ex)
            {
            }
            Expression<Func<Caregory, bool>> fun = t => t.Id > 0;
            fun = fun.And(t => t.Email.Length > 0);
            fun = fun.Or(t => t.Email.Length > 0);
            _iCategoryService.Query(fun);
            var model = _iCategoryService.Find<Caregory>(1);
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return Json(HttpContext.Session["Code"],JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
       
    }
}