using LY.SuperWSocket.Console.DataCenter;
using LY.SuperWSocket.Console.Models;
using LY.SuperWSocket.Console.Session;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LY.SuperWSocket.Console.Commands
{
    /// <summary>
    /// 客户端确认的
    /// </summary>
    public class Confirm : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {

            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 1)
            {
                string GuId = requestInfo.Parameters[0];
                ChatDateManager<string, ChatModel>.Remove($"{session.Id}_{session.Name}", GuId);
            }
            else
            {
                session.Send("Login Error");
            }
        }
    }
}
