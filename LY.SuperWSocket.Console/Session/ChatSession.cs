using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Text;

namespace LY.SuperWSocket.Console.Session
{
    public class ChatSession : AppSession<ChatSession>
    {
        public string Id { get; set; }
        public bool IsLogin { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public DateTime LoginTime { get; set; }

        public DateTime LastHBTime { get; set; }

        public bool IsOnLine
        {
            get
            {
                return this.LastHBTime.AddSeconds(10) > DateTime.Now;
            }
        }

        public override void Send(string message)
        {
            System.Console.WriteLine($"准备发送给{this.Id}：{message}");
            base.Send(message.Format());
        }
        protected override void OnSessionStarted()
        {
            this.Send("Welcome to SuperSocket Chat Server");
        }

        protected override void OnInit()
        {
            this.Charset = Encoding.GetEncoding("gb2312");
            base.OnInit();
        }
        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            System.Console.WriteLine("收到命令:" + requestInfo.Key.ToString());
            this.Send("不知道如何处理 " + requestInfo.Key.ToString() + " 命令");
        }


        /// <summary>
        /// 异常捕捉
        /// </summary>
        /// <param name="e"></param>
        protected override void HandleException(Exception e)
        {
            this.Send($"\n\r异常信息：{ e.Message}");
            //base.HandleException(e);
        }

        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="reason"></param>
        protected override void OnSessionClosed(CloseReason reason)
        {
            System.Console.WriteLine("链接已关闭。。。");
            base.OnSessionClosed(reason);
        }

    }
}
