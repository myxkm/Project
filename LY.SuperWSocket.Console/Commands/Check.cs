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
    /// 登入
    /// </summary>
    public class Check : CommandBase<ChatSession, StringRequestInfo>
    {
        public override void ExecuteCommand(ChatSession session, StringRequestInfo requestInfo)
        {

            if (requestInfo.Parameters != null && requestInfo.Parameters.Length == 3)
            {
                string Id = requestInfo.Parameters[0];
                string Name = requestInfo.Parameters[1];
                string PassWord = requestInfo.Parameters[2];

                ChatSession oldSession = session.AppServer.GetAllSessions().FirstOrDefault(t => Id.Equals(t.Id) && Name.Equals(t.Name) && PassWord.Equals(t.PassWord));
                if (oldSession != null)
                {
                    oldSession.Send("账号异常");
                    oldSession.Close();
                }
                session.Id = Id;
                session.Name = Name;
                session.PassWord = PassWord;
                session.IsLogin = true;
                session.LastHBTime = DateTime.Now;
                session.Send("Login Success");

                ChatDateManager<string, ChatModel>.SendLogin($"{Id}_{Name}", c =>
                {
                    session.Send($"在{c.CreateTime}时{c.FromId}：{c.FromName}给你发送消息：{c.Message} {c.GuId}");
                });
            }
            else
            {
                session.Send("Login Error");
            }
        }
    }
}
