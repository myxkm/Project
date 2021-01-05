using LY.Framework.LoggerHelper;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.JobQuartz
{
    public class JobTime : IJob
    {
        protected Logger logger = Logger.CreateLogger(typeof(JobTime));
        public virtual Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                logger.Debug(".............0...........", new Exception("JobTime   "));
            });
        }
    }

    /// <summary>
    /// 工作 定时任务
    /// </summary>
    public class JobTime_1_1 : JobTime
    {
        public override Task Execute(IJobExecutionContext context)
        {
            logger.Debug(".............1...........", new Exception("JobTime_1_1   "));
            return Task.Factory.StartNew(() => Auto());
        }
        public void Auto()
        {


        }
    }
    public class JobTime_2_1 : JobTime
    {
        public override Task Execute(IJobExecutionContext context)
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
            logger.Debug(".............2...........", new Exception("JobTime_2_1   "));
            return await Task.FromResult(num);
        }
    }

}
