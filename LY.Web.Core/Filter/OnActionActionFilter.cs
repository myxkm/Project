using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LY.Web.Core.Filter
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OnActionActionFilterAttribute : ActionFilterAttribute
    {
        //OnActionExecuting（）-> Action execute & return View() ->OnActionExecuted（） ->OnResultExecuting（） -> Render View() ->OnResultExecuted()
        /// <summary>
        /// 方法执行前 压缩传输
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            string acceptEncoding = request.Headers["Accept-Encoding"]; //获取浏览器的压缩格式
            if (string.IsNullOrEmpty(acceptEncoding)) return;
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            HttpResponseBase response = filterContext.HttpContext.Response;
            if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);//把信息压缩后传输
            }
            else if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }

        }
        /// <summary>
        /// 缓存时长 单位s
        /// </summary>
        private int _MaxSecond = 0;
        public OnActionActionFilterAttribute(int duration)
        {
            this._MaxSecond = duration;
        }
        public OnActionActionFilterAttribute()
        {
            this._MaxSecond = 60;
        }
        /// <summary>
        /// 方法执行后 缓存
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (this._MaxSecond <= 0) return;

            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            TimeSpan cacheDuration = TimeSpan.FromSeconds(this._MaxSecond);

            cache.SetCacheability(HttpCacheability.Public);
            //cache.SetLastModified(DateTime.Now.AddHours(8).Add(cacheDuration));
            //cache.SetExpires(DateTime.Now.AddHours(8).Add(cacheDuration));//GMT时间 格林威治时间 
            cache.SetExpires(DateTime.Now.Add(cacheDuration));
            cache.SetMaxAge(cacheDuration);
            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }
       
    }
}
