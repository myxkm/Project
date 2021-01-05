using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LY.Web.Core.PipeLine
{
    /// <summary>
    /// 要注册路由   扩展route，拒绝浏览器、IP 、检查referer
    /// </summary>
    public class CustomRoute : RouteBase
    {
        /// <summary>
        /// 解析路由信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            //httpContext.Request.UserHostAddress
            //禁用浏览器
            if (!httpContext.Request.UserAgent.Equals("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4181.9 Safari/537.36"))
            {
                RouteData rd = new RouteData(this, new MvcRouteHandler());
                rd.Values.Add("controller", "Home");
                rd.Values.Add("action", "Error");
                rd.Values.Add("Id", "1111");
                return rd;
            }
            return null;
        }

        /// <summary>
        /// 指定处理的虚拟路径
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
