using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace coreapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RedPacketController : CustomBaseController
    {
       
        private readonly ILogger<RedPacketController> _logger;
        public RedPacketController(ILogger<RedPacketController> logger)
        {
            _logger = logger;
        }
        public class ReModel
        {
            public bool Action { get; set; }
            public string Message { get; set; }
            public dynamic Data { get; set; }
            public decimal AllPrice { get; set; }
            public int Count { get; set; }
        }
        /// <summary>
        /// <![CDATA[红包算法1]]>
        /// </summary>
        /// <param name="price">发的金额</param>
        /// <param name="priceMin">每人枪的最小额</param>
        /// <param name="count">红包个数</param>
        /// <returns>获取红包池子</returns>
        [HttpGet]
        [AllowAnonymous]
        public ReModel Get(decimal price = 200m, decimal priceMin = 10m, int count = 20)
        {                    
            {
                ReModel model = new ReModel();
                int priceFen = Convert.ToInt32(price * 100);
                int minPriceFen = Convert.ToInt32(priceMin * 100);
                if (count * minPriceFen > priceFen)//最小金额对应的人的金额是否大于发的包
                {
                    model.Action = false;
                    model.Message = "最小金额对应的人的金额是否大于发的包";
                    return model;
                }
                List<int> redPacketList = new List<int>() { };
                for (int i = 0; i < count; i++)
                {
                    redPacketList.Add(minPriceFen);
                }
                priceFen -= count * minPriceFen;//1 剩余
                var rand = new Random();
                while (priceFen > 0)
                {
                    int priceFenRand = rand.Next(1, priceFen);
                    int redPacketRand = rand.Next(0, count);
                    redPacketList[redPacketRand] += priceFenRand;
                    priceFen -= priceFenRand;
                }
                decimal AllPrice = 0.00m;
                List<decimal> redPacketDecimalList = new List<decimal>() { };
                redPacketList.ForEach(t => {
                    var redPacketDecimal = Convert.ToDecimal(t / (100 * 1.00));
                    AllPrice += redPacketDecimal;
                    redPacketDecimalList.Add(redPacketDecimal);
                });

                model.Data = redPacketDecimalList;
                model.AllPrice = AllPrice;
                model.Count = redPacketList.Count;
                return model;
            }
        }


        /// <summary>
        /// <![CDATA[红包算法2 优化后 ]]>
        /// </summary>
        /// <param name="price">发的金额</param>
        /// <param name="priceMin">每人枪的最小额</param>
        /// <param name="count">红包个数</param>
        /// <returns>获取红包池子</returns>               
        [HttpGet]
        [AllowAnonymous]
        public ReModel Get1(decimal price = 2.00m, decimal priceMin = 0.01m, int count = 20)
        {   
            {
                ReModel model = new ReModel();
                int priceFen = Convert.ToInt32(price * 100);
                int minPriceFen = Convert.ToInt32(priceMin * 100);
                if (count * minPriceFen > priceFen)//最小金额对应的人的金额是否大于发的包
                {
                    model.Action = false;
                    model.Message = "最小金额对应的人的金额是否大于发的包";
                    return model;
                }
                var rand = new Random();
                List<int> redPacketList = new List<int>() { };
                for (int i = 0; i < count; i++)
                {
                    int svgpriceFen = priceFen / (count-i);
                    int priceFenRand = rand.Next(minPriceFen, svgpriceFen);
                    redPacketList.Add(priceFenRand);
                    priceFen -= priceFenRand;
                }
                if (priceFen>0)
                {
                    int redPacketRand = rand.Next(0, count);
                    redPacketList[redPacketRand] += priceFen;
                }
                decimal AllPrice = 0.00m;
                List<decimal> redPacketDecimalList = new List<decimal>() { };
                redPacketList.ForEach(t => {
                    var redPacketDecimal = Convert.ToDecimal(t / (100 * 1.00));
                    AllPrice += redPacketDecimal;
                    redPacketDecimalList.Add(redPacketDecimal);
                });
                model.Data = redPacketDecimalList;
                model.AllPrice = AllPrice;
                model.Count = redPacketList.Count;
                return model;
            }
        }




    }
}
