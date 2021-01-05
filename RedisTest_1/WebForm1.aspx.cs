 using MVC5Project.Redis.BaseOnStackExchage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RedisTest_1
{
    [Serializable]
    class ChatModels
    {
        public string userId { get; set; }
        public DateTime date { get; set; }
    }
    public class MessageQueue
    {
        static System.Timers.Timer timer = new System.Timers.Timer(5000);
        static ChatModels CurrentChatModels = new ChatModels();
        static MessageQueue()
        {
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Disposed += Timer_Disposed;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        private static void Timer_Disposed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        //private static void 
        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var redisClient = new RedisHelper(2);
            // 消息出列
            CurrentChatModels = redisClient.ListLeftPop<ChatModels>("MessageQuene");
        }
    }

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected static bool Qu;

        protected void Page_Load(object sender, EventArgs e)
        {
            var messageQueue = new MessageQueue();
            //var s = "a";
            ////new RedisHelper().Set("abc", s, 10);
            //var a = DateTime.Now;
            //var timeStamp = TimeHelp.GetTimeStamp(a, 17);
            //var isTime1 = TimeHelp.IsTime((timeStamp), 1);
            //Thread.Sleep(1000 * 60);
            //var isTime3 = TimeHelp.IsTime(timeStamp, 1);
            //Thread.Sleep(1000 * 60);
            //var isTime4 = TimeHelp.IsTime(timeStamp, 2);
            //var isTime5 = TimeHelp.IsTime(timeStamp, 2.2);
            //Thread thread = new Thread(run);
            //thread.Start();
            //Thread[] threads = new Thread[10];
            //for (int i = 0; i < threads.Length; i++)
            //{
            //    threads[i] = new Thread(Pull);
            //    threads[i].Start();
            //}

            var redisClient = new RedisHelper(2);
            var redisClient1 = new RedisHelper(1);
            var redisClient3 = new RedisHelper(3);
            var redisClient4 = new RedisHelper(4);






            if (!Qu)
            {
                Qu = true;
                Task.Run(() =>
                {
                    while (true)
                    {
                        // 消息出列
                        var CurrentChatModels = redisClient.ListLeftPop<ChatModels>("MessageQuene");
                        if (CurrentChatModels == null)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(60 * 10));
                        }
                        else
                        {
                            redisClient1.SortedSetAdd("log", CurrentChatModels, Convert.ToInt64(DateTime.Now.ToString("mmss")));
                        }
                    }

                });
            }


            string keyUserId = Request["keyUserId"];
            //for (int i = 0; i < 1000; i++)
            { //消息入列
                redisClient.ListRightPush("MessageQuene", new ChatModels { userId = keyUserId, date = DateTime.Now });
            }


            redisClient.Subscribe("x", (k, v) =>
            {
                var vv = v;
                redisClient3.ListRightPush("MessageQuene", v);
            });

            for (int i = 0; i < 20; i++)
            {
                redisClient.Publish("x", "");
            }
        }
    }



    /// <summary>
    /// 时间戳
    /// </summary>
    public class TimeHelp
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <param name="length">13 17</param>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time, int length = 13)
        {
            long ts = ConvertDateTimeToLong(time);
            return ts.ToString().Substring(0, length);
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToLong(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks);
            return t;
        }
        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            if (timeStamp.Length == 13) timeStamp += "0000";
            long lTime = long.Parse(timeStamp);
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 时间戳转为C#格式时间10位
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetDateTimeFrom1970Ticks(long curSeconds)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(curSeconds);
        }

        /// <summary>
        /// 验证时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <param name="interval">差值（分钟）</param>
        /// <returns></returns>
        public static bool IsTime(string timeStamp, double interval)
        {
            DateTime dt = ConvertStringToDateTime(timeStamp);
            //取现在时间
            DateTime dt1 = DateTime.Now.AddMinutes(interval);
            DateTime dt2 = DateTime.Now.AddMinutes(interval * -1);
            if (dt > dt2 && dt < dt1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断时间戳是否正确（验证前8位）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool IsTime(string time)
        {
            string str = GetTimeStamp(DateTime.Now, 8);
            if (str.Equals(time))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}