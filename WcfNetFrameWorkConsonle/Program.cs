using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Service;

namespace WcfNetFrameWorkConsonle
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                List<ServiceHost> serviceHostList = new List<ServiceHost> { };
                serviceHostList.Add(new ServiceHost(typeof(CompanyService)));
                serviceHostList.Add(new ServiceHost(typeof(DepartmentService)));
                serviceHostList.Add(new ServiceHost(typeof(ProductService)));
                serviceHostList.Add(new ServiceHost(typeof(UserService)));
                foreach (var host in serviceHostList)
                {
                    host.Opening += (s, e) => { Console.Write($" 打开ing e:{e.GetType().Name}     host:{host.GetType().Name}  "); };
                    host.Opened += (s, e) => { Console.Write($"已打开e:{e.GetType().Name}       host:{host.GetType().Name} "); };
                    host.Open();
                }
                Console.Write(".关闭，请按任意键....");
                Console.Read();
                foreach (var host in serviceHostList)
                {
                    host.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write(".......", ex.Message);

            }
            Console.Read();

        }
 
    }
}
