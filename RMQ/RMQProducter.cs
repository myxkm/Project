using RabbitMQ.Client;
using System;
using System.Text;

namespace RMQ
{
    /// <summary>
    /// 生产者
    /// </summary>
    public sealed class RMQProducter<T>
    {
        //ExchangeType.Topic
        //fanout:把所有发送到该Exchange的消息投递到所有与它绑定的队列中。
        //direct:把消息投递到那些binding key与routing key完全匹配的队列中。
        //topic:将消息路由到binding key与routing key模式匹配的队列中。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public static void SendMassageTopic(RequestModel<T> request)
        {
            //topic 是routingKey 正则匹配 只有 * 是一个字符 # 多个字符 
            Config config = new Config() { HostName = "127.0.0.1", Password = "ly001", Port = 5672, UserName = "ly001", ExchangeType = ExchangeType.Topic };
            ConnectionFactory connectionFactory = CustomConnectionFactory.Init(config);

            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: config.ExchangeName, type: config.ExchangeType, autoDelete: config.AutoDelete, durable: config.Durable, arguments: null);
                    channel.QueueDeclare(queue: config.QueueName, durable: config.Durable, exclusive: false, autoDelete: config.AutoDelete, arguments: null);
                    channel.QueueBind(queue: config.QueueName, exchange: config.ExchangeName, routingKey: config.RoutingKey, arguments: null);
                    //消息持久化
                    IBasicProperties basicProperties = channel.CreateBasicProperties();
                    basicProperties.DeliveryMode = 2;
                    while (true)
                    {
                        byte[] body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                        channel.BasicPublish(exchange: config.ExchangeName, routingKey: config.RoutingKey, basicProperties: basicProperties, body: body);
                        Console.WriteLine(string.Format("{0}", DateTime.Now.ToString("HH:mm:ss")));
                    }

                }
            }

        }

        /// <summary>
        ///  单点精确路由模式
        /// </summary>
        public static void SendMassageDirect(RequestModel<T> request)
        {
            Config config = new Config() { HostName = "127.0.0.1", Password = "ly001", Port = 5672, UserName = "ly001", ExchangeType = ExchangeType.Direct };
            ConnectionFactory connectionFactory = CustomConnectionFactory.Init(config);
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: config.ExchangeName, type: config.ExchangeType, autoDelete: config.AutoDelete, durable: config.Durable, arguments: null);

                    channel.QueueDeclare(queue: config.QueueName, durable: config.Durable, exclusive: false, autoDelete: config.AutoDelete, arguments: null);
                    channel.QueueBind(queue: config.QueueName, exchange: config.ExchangeName, routingKey: config.RoutingKey, arguments: null);
                    //消息持久化
                    IBasicProperties basicProperties = channel.CreateBasicProperties();
                    basicProperties.DeliveryMode = 2;
                    byte[] body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                    channel.BasicPublish(exchange: config.ExchangeName, routingKey: config.RoutingKey, basicProperties: basicProperties, body: body);
                    Console.WriteLine(string.Format("{0}", DateTime.Now.ToString("HH:mm:ss")));
                }
            }
        }

        /// <summary>
        ///  单点精确路由模式
        /// </summary>
        public static void SendMassageFanout(RequestModel<T> request)
        {
            Config config = new Config() { HostName = "127.0.0.1", Password = "ly001", Port = 5672, UserName = "ly001", ExchangeType = ExchangeType.Fanout };
            ConnectionFactory connectionFactory = CustomConnectionFactory.Init(config);
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //消息持久化
                    IBasicProperties basicProperties = channel.CreateBasicProperties();
                    basicProperties.DeliveryMode = 2;
                    byte[] body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                    channel.BasicPublish(exchange: config.ExchangeName, routingKey: null, basicProperties: basicProperties, body: body);
                    Console.WriteLine(string.Format("{0}", DateTime.Now.ToString("HH:mm:ss")));
                }
            }
        }
    }
}
