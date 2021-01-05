using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.Framework.WorkContext
{
    public class AdminWorkContext
    {
        public bool IsLogin { get; set; }
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public CurrentUser CurrentUser { get; set; }

        public bool IsAjax { get; set; }

    }
}
