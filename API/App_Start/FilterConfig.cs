using API.Unility;
using LY.Web.Core.Filter;
using System.Web;
using System.Web.Mvc;

namespace API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //异常
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
