using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.JobQuartz
{
    public class Scheduler
    {
        private static  IScheduler _instance;
        private Scheduler()
        {
        }
        /// <summary>
        /// 获得本类实例的唯一全局访问点。
        /// </summary>
        /// <returns></returns>
        //[CLSCompliant(false)]
        public static async Task<IScheduler> GetIntance()
        {
            if (_instance == null)
            {
                var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
                ISchedulerFactory schedFact = new StdSchedulerFactory(props);
                _instance = await schedFact.GetScheduler();
            }
            return _instance;
        }
    }
}
