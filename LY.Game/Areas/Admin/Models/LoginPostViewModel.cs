using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LY.Game.Areas.Admin.Models
{
    public class LoginPostViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 是否需要验证码
        /// </summary>
        public bool IsVerfiyCode { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerfiyCode { get; set; }

        public string ErrorMsg { get; set; }
    }
}