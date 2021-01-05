using RabbitMQ.Client;
namespace RMQ
{
    public sealed class CustomConnectionFactory
    {
        private static ConnectionFactory _factory = null;
        private static object obj = new object();
        private CustomConnectionFactory() { }
        public static ConnectionFactory Init(Config config)
        {
            if (_factory == null)
            {
                lock (obj)
                {
                    if (_factory == null)
                    {
                        _factory = new ConnectionFactory
                        {
                            HostName = config.HostName,
                            UserName = config.UserName,
                            Password = config.Password,
                            Port = config.Port,
                        };
                    }
                }
            }
            return _factory;
        }
    }
}
