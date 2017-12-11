using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
            printInstructions();

            try
            {
                return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls("http://*:80")
                    .Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Defaulting to port 5000");
            }

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }

        private static void printInstructions()
        {
            Console.WriteLine("Please try the following urls below in your browser to access the site:");
            printLocalIpAddress();
            Console.WriteLine("");
        }

        private static void printLocalIpAddress()
        {
            foreach(NetworkInterfaceType networkInterfaceType in Enum.GetValues(typeof(NetworkInterfaceType)))
            {
                printLocalIPAddressBasedOnType(networkInterfaceType);
            }
        }

        private static void printLocalIPAddressBasedOnType(NetworkInterfaceType networkInterfaceType)
        {
            foreach(var ipAddress in GetAllLocalIPv4(networkInterfaceType))
            {
                Console.WriteLine(ipAddress);
            }
        }

        // https://stackoverflow.com/questions/6803073/get-local-ip-address
        private static string[] GetAllLocalIPv4(NetworkInterfaceType _type)
        {
            List<string> ipAddrList = new List<string>();
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddrList.Add(ip.Address.ToString());
                        }
                    }
                }
            }
            return ipAddrList.ToArray();
        }
    }
}
