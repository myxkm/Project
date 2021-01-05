using LY.MVC.Utility;
using LY.Web.Core.Filter;
using System.Web;
using System.Web.Mvc;

namespace LY.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //异常
            filters.Add(new CustomHandleErrorAttribute());
            //注册所有权限的控制器
            filters.Add(new CustomAuthorizeAttribute());
            ///
            filters.Add(new CustomActionFilterAttribute());


            filters.Add(new OnActionActionFilterAttribute());

            
        }
    }
}
