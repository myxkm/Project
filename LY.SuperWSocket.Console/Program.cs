using LY.Framework.Comman;
using Newtonsoft.Json.Linq;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.SuperWSocket.Console
{
    class Program
    {
        //commandname 空格 参数 空格 参数 加\r\n
        //心跳
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("data.txt");
            string input = sr.ReadToEnd();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(input);
            var status = result["status"].ToString();
            var environment = result["environment"].ToString();
            var receipt = result["receipt"];
            var receipt_receipt_type = receipt["receipt_type"].ToString();
            var receipt_adam_id = receipt["adam_id"].ToString();
            //取最新的交易时间的，而且价格匹配上，进行去充值操作，额外数据的进行数据存储，方便后续用户投诉时，进行查找凭证。
            var receipt_in_app = receipt["in_app"].OrderByDescending(t => t["original_purchase_date"].ToString()).Select(t => new
            {
                product_id = t["product_id"].ToString(),
                original_purchase_date = t["original_purchase_date"].ToString()
            }).ToArray();
            if (receipt_in_app.Count() > 0)
            {
                var receipt_in_app_product_id = receipt_in_app[0].product_id;
                var receipt_in_original_purchase_date = receipt_in_app[0].original_purchase_date;
                var original_purchase_date = Convert.ToDateTime(receipt_in_original_purchase_date.Substring(0, receipt_in_original_purchase_date.IndexOf('E')));


            }




            new SuperSocketMain().Init();
        }
    }
}
