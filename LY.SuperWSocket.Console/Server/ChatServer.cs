using System;
using System.Collections.Generic;
using LY.SuperWSocket.Console.CommandsFilter;
using LY.SuperWSocket.Console.Session;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace LY.SuperWSocket.Console.Server
{
    [AuthorisizeFilter]
    public class ChatServer : AppServer<ChatSession>
    {
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            System.Console.WriteLine("准备读取配置文件。。。。");
            return base.Setup(rootConfig, config);
        }
        protected override void OnStarted()
        {
            System.Console.WriteLine("Chat服务启动。。。");
            base.OnStarted();
        }
        protected override void OnStopped()
        {
            System.Console.WriteLine("Chat服务停止。。。");
            base.OnStopped();
        }
        protected override void OnNewSessionConnected(ChatSession session)
        {
            System.Console.WriteLine("Chat服务新加入的连接:" + session.LocalEndPoint.Address.ToString());
            base.OnNewSessionConnected(session);
        }
    }
}
