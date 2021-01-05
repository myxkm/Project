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
    /// HeartBeat   心跳
    /// </summary>
    public class HB : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 1)
            {
                string c = requestInfo.Parameters[0];
                if ("$".Equals(c))
                {
                    session.LastHBTime = DateTime.Now;
                    session.Send("$");
                }
                else
                {
                    session.Send("参数不对");
                }
            }
            else
            {
                session.Send("参数不对");
            }
        }
    }
}
