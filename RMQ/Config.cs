using RabbitMQ.Client;

namespace RMQ
{
    public class Config
    {
        /// <summary>
        /// 交换机（路由）的名字
        /// </summary>
        public string ExchangeName { get; set; } = "xkm.exchange";
        /// <summary>
        /// 队列的名字
        /// </summary>
        public string QueueName { get; set; } = "xkm.queue";

        /// <summary>
        /// 交换机 对应的 RoutingKey
        /// </summary>
        public string RoutingKey { get; set; } = "xkm.routingkey";

        /// <summary>
        /// 交换机（路由）类型 
        /// </summary>
        public string ExchangeType { get; set; }
        /// <summary>
        /// 是否为持久化
        /// </summary>
        public bool Durable { get; set; } = true;
        /// <summary>
        /// 是否自动删除
        /// </summary>
        public bool AutoDelete { get; set; }

        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
