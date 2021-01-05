using LY.Bussiness.Interface;
using LY.EF.Model;
using LY.Web.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
namespace LY.MVC.Utility
{
    public static class UserManager
    {
        public static bool Login(this HttpContext context, string account, string pwd, string verifycode)
        {
            var code = context.Session["Code"];
            if (code != null&&!string.IsNullOrEmpty(code.ToString())&& code.ToString().Equals(verifycode,StringComparison.CurrentCultureIgnoreCase)) {
                using (IUserService service = DIFactory.GetContainer().Resolve<IUserService>())
                {
                    service.Set<User>().Where(t => t.Email.Length > 0);
                }
            }
            return false;
        }
    }
}