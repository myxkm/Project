using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using log4net.Config;
using log4net;
using Newtonsoft.Json;
using System.Collections.Generic;
using log4net.Repository;
using System.Threading.Tasks;
using System.Text;

namespace LY.Framework.LoggerHelper
{
    public class Logger
    {


        private static TaskFactory taskFactory = new TaskFactory();
        static Logger()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\log4net.config.xml")));
            ILog Log = LogManager.GetLogger(typeof(Logger));
            Log.Info("系统初始化Logger模块");
        }

        private ILog logger = null;
        private Logger(Type type)
        {
            logger = LogManager.GetLogger(type);
        }

        /// <summary>
        /// 创建Logger
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Logger CreateLogger(Type type)
        {
            return new Logger(type);
        }

        /// <summary>
        /// Log4日志 出现严重异常
        /// </summary>
        /// <param name="msg">出现严重异常</param>
        /// <param name="exception"></param>
        public void Fatal(object msg, Exception exception = null)
        {
            logger.Fatal(msg, exception);
        }
        /// <summary>
        /// Log4日志 "出现异常"
        /// </summary>
        /// <param name="msg">出现异常</param>
        /// <param name="exception"></param>
        public void Error(object msg, Exception exception = null)
        {
            logger.Error(msg, exception);
        }

        /// <summary>
        /// Log4日志  警告
        /// </summary>
        /// <param name="msg">警告</param>
        /// <param name="exception"></param>
        public void Warn(object msg, Exception exception = null)
        {
            logger.Warn(msg, exception);
        }

        /// <summary>
        /// Log4日志  详情
        /// </summary>
        /// <param name="msg">详情</param>
        /// <param name="exception"></param>
        public void Info(object msg, Exception exception = null)
        {
            logger.Info(msg, exception);
        }
        /// <summary>
        /// Log4日志  Debug
        /// </summary>
        /// <param name="msg">Debug</param>
        /// <param name="exception"></param>
        public void Debug(object msg, Exception exception = null)
        {
            logger.Debug(msg, exception);
        }

        /// <summary>
        /// Log4日志 出现严重异常
        /// </summary>
        /// <param name="msg">出现严重异常</param>
        /// <param name="ex"></param>
        public void AsyncFatal(object msg, Exception exception = null)
        {
            taskFactory.StartNew(() => logger.Fatal(msg, exception));
        }
        /// <summary>
        /// Log4日志 "出现异常"
        /// </summary>
        /// <param name="msg">出现异常</param>
        /// <param name="exception"></param>
        public void AsyncError(object msg, Exception exception = null)
        {
            taskFactory.StartNew(() => logger.Error(msg, exception));
        }
        /// <summary>
        /// Log4日志  警告
        /// </summary>
        /// <param name="msg">警告</param>
        /// <param name="exception"></param>
        public void AsyncWarn(object msg, Exception exception = null)
        {
            taskFactory.StartNew(() => logger.Warn(msg, exception));
        }
        /// <summary>
        /// Log4日志  详情
        /// </summary>
        /// <param name="msg">详情</param>
        /// <param name="exception"></param>
        public void AsyncInfo(object msg, Exception exception = null)
        {
            taskFactory.StartNew(() => logger.Info(msg, exception));
        }
        /// <summary>
        /// Log4日志  Debug
        /// </summary>
        /// <param name="msg">Debug</param>
        /// <param name="exception"></param>
        public void AsyncDebug(object msg, Exception exception = null)
        {
            taskFactory.StartNew(() => logger.Debug(msg, exception));
        }












        /// <summary>
        /// Log4日志 出现严重异常
        /// </summary>
        /// <param name="msg">出现严重异常</param>
        public void Fatal(Exception exception)
        {
            logger.Fatal(WriteLog(exception));
        }
        /// <summary>
        /// Log4日志 "出现异常"
        /// </summary>
        /// <param name="exception">出现异常</param>
        public void Error(Exception exception)
        {
            logger.Error(WriteLog(exception));
        }
        /// <summary>
        /// Log4日志  警告
        /// </summary>
        /// <param name="exception">警告</param>
        public void Warn(Exception exception)
        {
            logger.Warn(WriteLog(exception));
        }
        /// <summary>
        /// Log4日志  详情
        /// </summary>
        /// <param name="exception">详情</param>
        public void Info(Exception exception)
        {
            logger.Info(WriteLog(exception));
        }
        /// <summary>
        /// Log4日志  Debug
        /// </summary>
        /// <param name="exception">Debug</param>
        public void Debug(Exception exception)
        {
            logger.Debug(WriteLog(exception));
        }
        /// <summary>
        /// Log4日志 出现严重异常
        /// </summary>
        /// <param name="ex">出现严重异常</param>
        public void AsyncFatal(Exception exception)
        {
            taskFactory.StartNew(() => logger.Fatal(WriteLog(exception)));
        }
        /// <summary>
        /// Log4日志 "出现异常"
        /// </summary>
        /// <param name="exception">出现异常</param>
        public void AsyncError(Exception exception)
        {
            taskFactory.StartNew(() => logger.Error(WriteLog(exception)));
        }
        /// <summary>
        /// Log4日志  警告
        /// </summary>
        /// <param name="exception">警告</param>
        public void AsyncWarn(Exception exception)
        {
            taskFactory.StartNew(() => logger.Warn(WriteLog(exception)));
        }
        /// <summary>
        /// Log4日志  详情
        /// </summary>
        /// <param name="exception">详情</param>
        public void AsyncInfo(Exception exception)
        {
            taskFactory.StartNew(() => logger.Info(WriteLog(exception)));
        }
        /// <summary>
        /// Log4日志  Debug
        /// </summary>
        /// <param name="exception">Debug</param>
        public void AsyncDebug(Exception exception)
        {
            taskFactory.StartNew(() => logger.Debug(WriteLog(exception)));
        }

        string WriteLog(Exception exception)
        {
            DateTime dt = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼");
            sb.AppendLine("");
            sb.AppendLine("Error DateTime：" + dt.ToString("yyyy-MM-dd HH:mm:ss fff"));
            sb.AppendLine("");
            sb.AppendLine("Error Code：" + exception.GetBaseException().GetHashCode());
            sb.AppendLine("");
            sb.AppendLine("Error Message：" + exception.GetBaseException().Message);
            sb.AppendLine("");
            sb.AppendLine("Error Source：" + exception.GetBaseException().Source);
            sb.AppendLine("");
            sb.AppendLine("Error StackTrace：" + exception.GetBaseException().StackTrace);
            sb.AppendLine("");
            sb.AppendLine("Error Ect： ");
            sb.AppendLine("");
            sb.AppendLine("Error Code：" + exception.GetHashCode());
            sb.AppendLine("");
            sb.AppendLine("Error Message： " + exception.Message);
            sb.AppendLine("");
            sb.AppendLine("Error Source： " + exception.Source);
            sb.AppendLine("");
            sb.AppendLine("Error StackTrace： " + exception.StackTrace);
            sb.AppendLine("");
            sb.AppendLine("▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲");
            string strXML = sb.ToString();
            return strXML;
        }
    }
}
