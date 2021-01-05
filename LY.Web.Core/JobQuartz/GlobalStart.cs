﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.Web.Core.JobQuartz
{
    public class GlobalStart
    {
        /// <summary>
        /// 开始作业
        /// </summary>
        public async static void Start()
        {
            //http://www.cnblogs.com/drift-ice/p/3817269.html
            //http://cron.qqe2.com/  在线生成表达式
            ///Quartz定时调度CronTrigger时间配置格式说明与实例
            await JobHelper.StartJob<JobTime_1_1>("0/2 0/1 * * * ? *");
            await JobHelper.StartJob<JobTime_2_1>("0/2 0/1 * * * ? *");
        }
        /// <summary>
        /// 停止作业
        /// </summary>
        public async static void End()
        {
            await JobHelper.StopJob<JobTime_1_1>();
            await JobHelper.StopJob<JobTime_2_1>();
        }
    }
}