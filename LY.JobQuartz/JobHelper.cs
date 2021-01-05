using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.JobQuartz
{
    public class JobHelper
    {

        #region 命名前缀(可以自行设置)
        /// <summary>
        ///     作业前缀
        /// </summary>
        private static string JobPerfix = "Job_";
        /// <summary>
        ///     作业分组前缀
        /// </summary>
        private static string GroupPerfix = "Group_";
        /// <summary>
        ///     作业触发器前缀
        /// </summary>
        private static string TriggerPerfix = "Trigger_";
        #endregion
        private static Task<IScheduler> _scheduler = Scheduler.GetIntance();

        #region 初始化作业实体
        /// <summary>
        ///     根据传入类型初始化作业实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static JobKey GetJobKey<T>()
        {
            string name = typeof(T).FullName;
            return new JobKey(JobPerfix + name, GroupPerfix + name);
        }
        #endregion 初始化作业实体
        #region 开启作业
        /// <summary>
        ///     开启作业
        /// </summary>
        /// <param name="expression">表达式
        /// <returns></returns>
        public async static Task StartJob<T>(string expression) where T : IJob
        {
            IScheduler scheduler = await _scheduler;
            JobKey jobKey = GetJobKey<T>();
            string name = typeof(T).FullName;
            IJobDetail job = await scheduler.GetJobDetail(jobKey);
            if (job != null && !await scheduler.IsJobGroupPaused(GroupPerfix + name))
            {
                return;
            }
            if (!scheduler.IsStarted)
            {
                await scheduler.Start();
            }
            if (job != null)
            {
                await scheduler.ResumeJob(jobKey);
            }
            else
            {
                IJobDetail detail = JobBuilder.Create<T>()
                    .WithIdentity(JobPerfix + name, GroupPerfix + name)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(TriggerPerfix + name)
                    .StartNow()
                    .WithCronSchedule(expression)
                    .ForJob(detail)
                    .Build();
                await scheduler.ScheduleJob(detail, trigger);
            }
        }
        #endregion

        #region 停止作业
        /// <summary>
        ///     停止作业
        /// </summary>
        /// <returns></returns>
        public async static Task<bool> StopJob<T>()
        {
            IScheduler scheduler = await _scheduler;
            JobKey jobKey = GetJobKey<T>();
            string name = typeof(T).FullName;
            IJobDetail detail = await scheduler.GetJobDetail(jobKey);
            if (detail != null && !await scheduler.IsJobGroupPaused(GroupPerfix + name))
            {
                await scheduler.PauseJob(jobKey);
                //await scheduler.Shutdown(true);
                return true;
            }
            return false;
        }
        #endregion

        #region 删除作业
        /// <summary>
        ///     删除作业
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async static Task<bool> RemoveJob<T>()
        {
            IScheduler scheduler = await _scheduler;
            JobKey jobKey = GetJobKey<T>();
            string name = typeof(T).FullName;
            IJobDetail detail = await scheduler.GetJobDetail(jobKey);
            if (detail != null && !await scheduler.IsJobGroupPaused(GroupPerfix + name))
            {
                await scheduler.PauseJob(jobKey);
                return await scheduler.DeleteJob(jobKey);
            }
            return false;
        }
        #endregion

        #region 取得作业运行状态
        /// <summary>
        /// 取得作业运行状态 true 正在运行，false 未在运行
        /// </summary>
        /// <returns></returns>
        public async static Task<bool> GetJobStatus<T>()
        {
            IScheduler scheduler = await _scheduler;
            JobKey jobKey = GetJobKey<T>();
            string name = typeof(T).FullName;
            IJobDetail detail = await scheduler.GetJobDetail(jobKey);
            return detail != null && !await scheduler.IsJobGroupPaused(GroupPerfix + name);
        }
        public async static Task<bool> GetJobStatus(string jobName)
        {
            IScheduler scheduler = await _scheduler;
            string className = jobName.Substring(JobPerfix.Length);
            IJobDetail detail = await scheduler.GetJobDetail(new JobKey(jobName, GroupPerfix + className));
            return detail != null && !await scheduler.IsJobGroupPaused(GroupPerfix + className);
        }

        #endregion
    }
}
