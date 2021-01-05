using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace RMQCustomer
{
    class Program
    {

        /// <summary>
        /// 连接配置
        /// </summary>
        private static readonly ConnectionFactory rabbitMqFactory = new ConnectionFactory()
        {
            HostName = "127.0.0.1",
            UserName = "ly001",
            Password = "ly001",
            Port = 5672,

        };
        /// <summary>
        /// 路由名称
        /// </summary>
        const string ExchangeName = "justin.exchange";

        //队列名称
        const string QueueName = "justin.queue";

        /// <summary>
        /// 路由名称
        /// </summary>
        const string TopExchangeName = "topic.justin.exchange";

        //队列名称
        const string TopQueueName = "topic.justin.queue";


        static void Main(string[] args)
        {
            //DirectAcceptExchange();
            //DirectAcceptExchangeEvent();
            //DirectAcceptExchangeTask();
            TopicAcceptExchange();

            RMQ.RMQCustomer.CustomerTopic();
            Console.WriteLine("按任意值，退出程序");
            Console.ReadKey();
        }

        public static void DirectAcceptExchange()
        {
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
                    channel.QueueDeclare(QueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
                    channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
                    while (true)
                    {
                        BasicGetResult msgResponse = channel.BasicGet(QueueName, autoAck: true);
                        if (msgResponse != null)
                        {
                            var msgBody = Encoding.UTF8.GetString(msgResponse.Body.ToArray());
                            Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
                        }

                        //BasicGetResult msgResponse2 = channel.BasicGet(QueueName, noAck: false);

                        ////process message ...

                        //channel.BasicAck(msgResponse2.DeliveryTag, multiple: false);
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }
            }
        }

        public static void DirectAcceptExchangeEvent()
        {
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
                    channel.QueueDeclare(QueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
                    //channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var msgBody = Encoding.UTF8.GetString(ea.Body.ToArray());
                        Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
                    };
                    channel.BasicConsume(QueueName, autoAck: true, consumer: consumer);

                    //已过时用EventingBasicConsumer代替
                    //var consumer2 = new QueueingBasicConsumer(channel);
                    //channel.BasicConsume(QueueName, noAck: true, consumer: consumer);
                    //var msgResponse = consumer2.Queue.Dequeue(); //blocking
                    //var msgBody2 = Encoding.UTF8.GetString(msgResponse.Body);

                    Console.WriteLine("按任意值，退出程序");
                    Console.ReadKey();
                }
            }
        }

        public static void DirectAcceptExchangeTask()
        {
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
                    channel.QueueDeclare(QueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);//告诉broker同一时间只处理一个消息
                    //channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var msgBody = Encoding.UTF8.GetString(ea.Body.ToArray());
                        Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
                        int dots = msgBody.Split('.').Length - 1;
                        System.Threading.Thread.Sleep(dots * 1000);
                        Console.WriteLine(" [x] Done");
                        //处理完成，告诉Broker可以服务端可以删除消息，分配新的消息过来
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    //noAck设置false,告诉broker，发送消息之后，消息暂时不要删除，等消费者处理完成再说
                    channel.BasicConsume(QueueName, autoAck: false, consumer: consumer);

                    Console.WriteLine("按任意值，退出程序");
                    Console.ReadKey();
                }
            }
        }

        public static void TopicAcceptExchange()
        {
            using (IConnection conn = rabbitMqFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: TopExchangeName, type: ExchangeType.Topic, durable: true, autoDelete: false, arguments: null);
                    channel.QueueDeclare(queue: TopQueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    channel.QueueBind(queue: TopQueueName, exchange: TopExchangeName, routingKey: TopQueueName);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var msgBody = Encoding.UTF8.GetString(ea.Body.ToArray());
                        Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
                        int dots = msgBody.Split('.').Length - 1;
                        System.Threading.Thread.Sleep(dots * 1000);

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume(TopQueueName, autoAck: false, consumer: consumer);

                    Console.WriteLine("按任意值，退出程序");
                    Console.ReadKey();
                }
            }
        }

    }
}
