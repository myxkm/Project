using LY.Framework.Config;
using LY.Framework.LoggerHelper;
using LY.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LY.Web.Core.Filter
{
    /// <summary>
    /// ajax跟exception一致
    /// 检验登陆和权限的filter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 未登录时返还的地址
        /// </summary>
        private string _LoginPath = "";
        public CustomAuthorizeAttribute()
        {
            this._LoginPath = StaticConstant.LoginUrl;
        }

        public CustomAuthorizeAttribute(string loginPath)
        {
            this._LoginPath = loginPath;
        }
        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="filterContext"></param>
        private static Logger logger = Logger.CreateLogger(typeof(CustomAuthorizeAttribute));
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var context = filterContext.HttpContext;
            var currentUrl = context.Request.Url;
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                logger.Debug($"{currentUrl}");
                return;
            }
            var currentUser = context.Session[StaticConstant.CurrentUserSession];
            if (currentUser != null && !string.IsNullOrEmpty(currentUser.ToString()) && currentUser is CurrentUser)
            {
                logger.Debug((currentUser as CurrentUser).Name);
                return;
            }
            if (context.Request.IsAjaxRequest())
            {
                AjaxResult ajaxResult = new AjaxResult { Result = DoResult.NoAuthorization, PromptMsg = "没有登入，没有权限" };
                filterContext.Result = new JsonResult() { Data = ajaxResult };
            }
            context.Session[StaticConstant.CurrentUrlSession] = currentUrl;
            filterContext.Result = new RedirectResult(_LoginPath);
            //base.OnAuthorization(filterContext);    
        }

    }
}