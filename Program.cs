using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace eventy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            try
            {
                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls("http://*:80")
                    .Build();
            }
            catch (Exception)
            {
                Console.WriteLine("Defaulting to port 5000");
            }

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
