using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace coreapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                                         .AddJsonFile("appsettings.json")
                                         .Build();
            CreateHostBuilder(args, configuration).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfigurationRoot con) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls(con["Urls"]);
                });
    }
}
