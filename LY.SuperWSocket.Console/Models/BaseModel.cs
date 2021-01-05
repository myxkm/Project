using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.SuperWSocket.Console.Models
{
    public class BaseModel
    {
        /// <summary>
        ///聊天记录的唯一标示
        /// </summary>
        public string GuId => Guid.NewGuid().ToString("N");
    }
}
