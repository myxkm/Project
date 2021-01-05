using LY.Framework.LoggerHelper;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.Web.Core.JobQuartz
{
    /// <summary>
    /// 工作 定时任务
    /// </summary>
    public class JobTime_1_1 : IJob
    {
        Logger logger = Logger.CreateLogger(typeof(JobTime_1_1));

        public Task Execute(IJobExecutionContext context)
        {
            logger.Error(".............1...........", new Exception("JobTime_1   "));
            return Task.Factory.StartNew(() => Auto());
        }
        public void Auto()
        {
            int num = 0;

        }
    }


    public class JobTime_2_1 : IJob
    {
        Logger logger = Logger.CreateLogger(typeof(JobTime_2_1));

        public Task Execute(IJobExecutionContext context)
        {
            Action action = () =>
            {
                new Action(async () =>
                {
                    await Auto();
                })();
            };
            return Task.Factory.StartNew(action);
        }
        public async Task<int> Auto()
        {
            int num = 0;
            logger.Error(".............2...........", new Exception("JobTime_2   "));
            return await Task.FromResult(num);
        }
    }

}
