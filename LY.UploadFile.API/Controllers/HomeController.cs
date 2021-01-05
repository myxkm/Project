using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Web.Mvc;

namespace LY.UploadFile.API.Controllers
{

    public class WebSocketUser
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
    }
    public class WebSocketDemo
    {
        /// <summary>
        /// //静态变量，用来记录当前在线连接数。应该把它设计成线程安全的。
        /// </summary>
        private static int onlineCount = 0;
        public static int GetOnlineCount()
        {
            return onlineCount;
        }
        public static void AddOnlineCount()
        {
            onlineCount++;
        }
        public static void SubOnlineCount()
        {
            onlineCount--;
        }
        private WebSocketUser webSocketUser = null;

        public ConcurrentDictionary<string, WebSocketDemo> concurrentDictionary = new ConcurrentDictionary<string, WebSocketDemo> { };

        public void OnClose()
        {
            if (IsExists(webSocketUser))
            {
                var isRemove = concurrentDictionary.TryRemove(webSocketUser.UserCode, out WebSocketDemo webSocketDemo);
                if (isRemove)
                    SubOnlineCount();
            }

        }
        public bool IsExists(WebSocketUser webSocketUser)
        {
            return concurrentDictionary.Keys.Contains(webSocketUser.UserCode);
        }

        public void OnOpen(WebSocketUser webSocketUser)
        {
            this.webSocketUser = webSocketUser;
            if (IsExists(webSocketUser))
            {
                var old = concurrentDictionary[webSocketUser.UserCode];
                concurrentDictionary.TryUpdate(webSocketUser.UserCode, this, old);
            }
            else
            {
                var isAdd = concurrentDictionary.TryAdd(webSocketUser.UserCode, this);
                if (isAdd)
                    AddOnlineCount();
            }
        }

        public void SendMessage(string message)
        {
            //webSocketUser.UserCode(message);
        }
        public void OnMessage(string message, WebSocketUser webSocketUser)
        {
            foreach (var item in concurrentDictionary)
            {
                item.Value.SendMessage(message);
            }
        }

    }



    public class HomeController : Controller
    {


        /// <summary>
        /// 长转短 链接的 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // GET: Home
        public dynamic Index(string Id)
        {
            //var id = Converter.To10(Id);
            //var user = new Converter().UserInfo((long)id);
            //return new { Url = user.Url};
            return "文件服务器";
        }
        public dynamic Z(string Url)
        {
            var user = new Converter().UserInfo(Url);
            if (user == null)  user= new Converter().Add(Url);
            return new { To62 = Converter.To62(user.Id) };
        }

        public class Users
        {
            public long Id { get; set; }
            public string Url { get; set; }
        }

        public class Converter
        {
            public static List<Users> UserList()
            {
                var list = new List<Users> {
                    new Users { Id = 100000, Url = "http://www.baidu.com" },
                    new Users { Id = 300000, Url = "http://www.baidu.com?1" }, };
                return list;
            }
            public Users Add(string Url)
            {
                var user = new Users { Id = 500000, Url = Url };
                UserList().Add(user);
                return user;
            }
            public Users UserInfo(long Id)
            {
                return UserList().FirstOrDefault(testc => testc.Id == Id);
            }
            public Users UserInfo(string Url)
            {
                return UserList().FirstOrDefault(testc => testc.Url == Url);
            }

            private static string keys = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            private static int exponent = keys.Length;//幂数
            /// <summary>
            /// decimal value type to 62 string
            /// </summary>
            /// <param name="value">The max value can not more decimal.MaxValue<</param>
            /// <returns>Return a specified 62 encode string</returns>
            public static string To62(decimal value)//17223472558080896352ul
            {
                string result = string.Empty;
                do
                {
                    decimal index = value % exponent;
                    result = keys[(int)index] + result;
                    value = (value - index) / exponent;
                }
                while (value > 0);

                return result;
            }


            /// <summary>
            /// 62 encode string to decimal
            /// </summary>
            /// <param name="value">62 encode string</param>
            /// <returns>Return a specified decimal number that decode by 62 string</returns>
            public static decimal To10(string value)//bUI6zOLZTrj
            {
                decimal result = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    int x = value.Length - i - 1;
                    result += keys.IndexOf(value[i]) * Pow(exponent, x);// Math.Pow(exponent, x);
                }
                return result;
            }

            /// <summary>
            /// 一个数据的N次方
            /// </summary>
            /// <param name="x"></param>
            /// <returns></returns>
            private static decimal Pow(decimal baseNo, decimal x)
            {
                decimal value = 1;////1 will be the result for any number's power 0.任何数的0次方，结果都等于1
                while (x > 0)
                {
                    value = value * baseNo;
                    x--;
                }
                return value;
            }

        }
    }
}