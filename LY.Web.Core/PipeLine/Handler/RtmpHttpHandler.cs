using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LY.Web.Core.PipeLine
{
    /// <summary>
    ///  您将需要在网站的 Web.config 文件中配置此处理程序 
    ///     routes.IgnoreRoute("Custom/{*path}");
    ///     <add name="rtmp" type="LY.Web.Core.PipeLine.RtmpHttpHandler,LY.Web.Core" path="*.rtmp"  verb="*" />
    ///     //路由配置 只要地址用的Custom 开头的都忽略
    /// </summary>
    public class RtmpHttpHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("RTMP请求");
        }
    }
}
