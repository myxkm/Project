using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.SuperWSocket.Console.Models
{
    /// <summary>
    /// 聊天的记录信息
    /// </summary>
    public class ChatModel : BaseModel
    {
        /// <summary>
        /// 信息来源的编号
        /// </summary>
        public string FromId { get; set; }
        /// <summary>
        /// 信息来源的名称
        /// </summary>
        public string FromName { get; set; }
        /// <summary>
        /// 目标编号
        /// </summary>
        public string ToId { get; set; }
        /// <summary>
        /// 目标名称
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// 发送的信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTime CreateTime => DateTime.Now;

        /// <summary>
        /// 发送的状态 0 未发生 1 已发送待确定 2 已确认
        /// </summary>
        public int State { get; set; }
    }


}
