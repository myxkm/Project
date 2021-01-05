using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RMQ
{
    public sealed class RMQCustomer
    {
        public static void CustomerTopic()
        {
            Config config = new Config() { HostName = "127.0.0.1", Password = "ly001", Port = 5672, UserName = "ly001", ExchangeType = ExchangeType.Topic };
            ConnectionFactory connectionFactory = CustomConnectionFactory.Init(config);
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: config.ExchangeName, type: config.ExchangeType, durable: config.Durable, autoDelete: config.AutoDelete, arguments: null);
                    channel.QueueDeclare(queue: config.QueueName, durable: config.Durable, exclusive: false, autoDelete: config.AutoDelete, arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    channel.QueueBind(queue: config.QueueName, exchange: config.ExchangeName, routingKey: config.RoutingKey, arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (o, e) =>
                    {
                        var data = Encoding.UTF8.GetString(e.Body.ToArray());
                        Console.WriteLine(string.Format("接收时间:{0}，内容：{1}", DateTime.Now.ToString("HH:mm:ss"), data));
                        channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume(queue: config.QueueName, autoAck: false, consumer: consumer);
                    Console.WriteLine("按任意值，退出程序");
                    Console.ReadKey();
                }

            }
        }

        public static void CustomerDirect()
        {
            Config config = new Config() { HostName = "127.0.0.1", Password = "ly001", Port = 5672, UserName = "ly001", ExchangeType = ExchangeType.Direct };
            ConnectionFactory connectionFactory = CustomConnectionFactory.Init(config);
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: config.ExchangeName, type: config.ExchangeType, durable: config.Durable, autoDelete: config.AutoDelete, arguments: null);
                    channel.QueueDeclare(queue: config.QueueName, durable: config.Durable, exclusive: false, autoDelete: config.AutoDelete, arguments: null);
                    channel.QueueBind(queue: config.QueueName, exchange: config.ExchangeName, routingKey: config.RoutingKey, arguments: null);
                    while (true)
                    {
                        BasicGetResult msgResponse = channel.BasicGet(queue: config.QueueName, autoAck: true);
                        if (msgResponse != null)
                        {
                            var msgBody = Encoding.UTF8.GetString(msgResponse.Body.ToArray());
                            Console.WriteLine(string.Format("接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
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
            Config config = new Config() { HostName = "127.0.0.1", Password = "ly001", Port = 5672, UserName = "ly001", ExchangeType = ExchangeType.Topic };
            ConnectionFactory connectionFactory = CustomConnectionFactory.Init(config);
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
                    channel.QueueDeclare(queue: config.QueueName, durable: config.Durable, exclusive: false, autoDelete: config.AutoDelete, arguments: null);
                    //channel.QueueBind(QueueName, ExchangeName, routingKey: QueueName);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var msgBody = Encoding.UTF8.GetString(ea.Body.ToArray());
                        Console.WriteLine(string.Format("***接收时间:{0}，消息内容：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msgBody));
                    };
                    channel.BasicConsume(queue: config.QueueName, autoAck: true, consumer: consumer);

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
            Config config = new Config() { HostName = "127.0.0.1", Password = "ly001", Port = 5672, UserName = "ly001", ExchangeType = ExchangeType.Topic };
            ConnectionFactory connectionFactory = CustomConnectionFactory.Init(config);
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //channel.ExchangeDeclare(ExchangeName, "direct", durable: true, autoDelete: false, arguments: null);
                    channel.QueueDeclare(queue: config.QueueName, durable: true, autoDelete: false, exclusive: false, arguments: null);
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
                    channel.BasicConsume(queue: config.QueueName, autoAck: false, consumer: consumer);

                    Console.WriteLine("按任意值，退出程序");
                    Console.ReadKey();
                }
            }
        }

    }
}
