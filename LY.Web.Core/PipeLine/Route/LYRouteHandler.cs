using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace LY.Web.Core.PipeLine
{
    /// <summary>
    /// 扩展IRouteHandler，
    /// 扩展IHttpHandler
    /// </summary>
    public class LYRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new LYHttpHandler(requestContext);
        }

    }
    /// <summary>
    /// 还是我们熟悉的handler
    /// </summary>
    public class LYHttpHandler : IHttpHandler
    {
        public LYHttpHandler(RequestContext requestContext)
        {
            Console.WriteLine("构造LYHttpHandler");
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //string url = context.Request.Url.AbsoluteUri;
            //context.Response.Write(string.Format("这里是LY定制：{0}", this.GetType().Name));
            //context.Response.Write((string.Format("当前地址为：{0}", url)));
            File.WriteAllText(context.Server.MapPath("~/WebCopy.config"), File.ReadAllText(context.Server.MapPath("~/Web.config")));
            context.Response.Write(File.ReadAllText(context.Server.MapPath("~/WebCopy.config")));
        }
        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}