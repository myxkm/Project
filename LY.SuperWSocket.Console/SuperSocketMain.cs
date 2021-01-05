using LY.Framework.LoggerHelper;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.SuperWSocket.Console
{
    public class SuperSocketMain
    {
        protected Logger logger = Logger.CreateLogger(typeof(SuperSocketMain));
        public virtual void Init()
        {
            IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                logger.Error("---初始化失败----");
                System.Console.Write($"---{0}初始化失败---");
                System.Console.ReadKey();
                return;
            }
            StartResult result = bootstrap.Start();
            foreach (var server in bootstrap.AppServers)
            {
                if (server.State.Equals(ServerState.Running))
                {
                    logger.Info($"---{server.Name}运行中---".Format());
                    System.Console.Write($"---{server.Name}运行中---".Format());
                }
                else
                {
                    logger.Info($"---{server.Name}启动失败---".Format());
                    System.Console.Write($"---{server.Name}启动失败---".Format());
                }
            }
            System.Console.ReadKey();
            System.Console.Read();
        }
    }
}
