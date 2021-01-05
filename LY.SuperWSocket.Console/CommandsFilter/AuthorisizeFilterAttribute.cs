using LY.SuperWSocket.Console.Server;
using LY.SuperWSocket.Console.Session;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.SuperWSocket.Console.CommandsFilter
{

    /// <summary>
    /// 过滤器  
    /// </summary>
    public class AuthorisizeFilterAttribute : CommandFilterAttribute
    {
        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            ChatSession session = (ChatSession)commandContext.Session;
            string name = commandContext.CurrentCommand.Name;
            if (!session.IsLogin)
            {
                if (!"Check".Equals(name,StringComparison.CurrentCultureIgnoreCase))
                {
                    session.Send("请登入");
                    commandContext.Cancel = true;
                }
                else
                {

                }
            }
            else if(!session.IsOnLine)
            {
                session.LastHBTime = DateTime.Now;
            }
            else
            {

            }

        }

        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {

        }
    }
}
