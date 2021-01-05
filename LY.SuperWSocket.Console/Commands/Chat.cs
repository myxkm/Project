using LY.SuperWSocket.Console.CommandsFilter;
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
    /// 发送消息
    /// </summary>
    [AuthorisizeFilter]
    public class Chat : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {
            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 3)
            {
                string toId = requestInfo.Parameters[0];
                string toName = requestInfo.Parameters[1];
                string Message = requestInfo.Parameters[2];
                ChatSession toSession = session.AppServer.GetAllSessions().FirstOrDefault(t => toId.Equals(t.Id) && toName.Equals(t.Name));
                ChatModel model = new ChatModel
                {
                    FromId = session.Id,
                    FromName = session.Name,
                    ToId = toId,
                    ToName = toName,
                    Message = Message,
                };
                if (toSession != null)//在线消息
                {
                    model.State =1;
                    ChatDateManager<string, ChatModel>.Add($"{toId}_{toName}", model);
                    toSession.Send($"{session.Id}_{session.Name}给你发消息：{Message} {model.GuId}");
                }
                else//离线消息
                {
                    model.State = 0;
                    session.Send($"{toId}_{toName}不在线");

                }
            }
            else
            {
                session.Send("参数不对");
            }
        }
    }
}
