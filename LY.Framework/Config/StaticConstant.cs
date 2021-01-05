using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.Framework.Config
{
    public static class StaticConstant
    {
        public static string IndexPath = ConfigurationManager.AppSettings["IndexPath"];
        public static string CurrentUserSession = ConfigurationManager.AppSettings["CurrentUserSession"];
        public static string CurrentUrlSession = ConfigurationManager.AppSettings["CurrentUrlSession"];
        public static string LoginUrl = ConfigurationManager.AppSettings["LoginUrl"];

        /// <summary>
        ///   /*加密解密相关配置*/
        /// </summary>
        public static string EncryptKey = ConfigurationManager.AppSettings["EncryptKey"];

    }
}
