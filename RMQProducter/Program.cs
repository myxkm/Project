using System;

namespace RMQProducter
{
    class Program
    {
        static void Main(string[] args)
        {
            string vadata = Console.ReadLine();
            while (vadata != "Exit")
            {
                RMQ.RMQProducter<string>.SendMassageTopic(new RMQ.RequestModel<string> { Data = vadata, Type = RMQ.RequestType.order });
                vadata = Console.ReadLine();
            }
        }
    }
}
