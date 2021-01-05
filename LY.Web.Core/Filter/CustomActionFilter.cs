using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LY.Web.Core.Filter
{

    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        #region Identity
        private Stopwatch timerAction = new Stopwatch();
        private Stopwatch timerResult = new Stopwatch();
        private int _MaxSecond = 60;
        private bool _IsShow = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isShow"></param>
        /// <param name="maxSecond">缓存时间</param>
        public CustomActionFilterAttribute(bool isShow = true,int maxSecond=60)
        {
            this._IsShow = isShow;
            this._MaxSecond = maxSecond;
        }
        #endregion
        /// <summary>
        /// 方法执行前  传输压缩文件到客户端  影响CPU
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
            if (this._IsShow)
            {
                timerAction.Start();
                filterContext.HttpContext.Response.Write($"<h1 style='color:#00f'>这里是OnActionExecuting :{DateTime.Now.ToString("yyyyMMdd - HHmmss.fff")}</h1><hr>");
            }
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

            if (this._IsShow)
            {
                timerAction.Stop();
                string message = $"<h1 style='color:#00f'>这里是OnActionExecuted:{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")},耗时{timerAction.ElapsedMilliseconds}</h1><hr>";
                filterContext.HttpContext.Response.Write(message);
            }
        }
        /// <summary>
        /// 方法返回前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (this._IsShow)
            {
                timerResult.Start();
                string message = $"<h1 style='color:#00f'>这里是OnResultExecuting:{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")},耗时{timerAction.ElapsedMilliseconds}</h1><hr>";
                filterContext.HttpContext.Response.Write(message);
            }
        }
        /// <summary>
        /// 方法返回后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (this._IsShow)
            {
                timerResult.Stop();
                string message = $"<h1 style='color:#00f'>这里是OnResultExecuted:{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")},耗时{timerAction.ElapsedMilliseconds}</h1><hr>";
                filterContext.HttpContext.Response.Write(message);
            }
        }
    }

}