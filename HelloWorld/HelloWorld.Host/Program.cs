using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using HelloWorld.Grains;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.CodeGeneration;
using Orleans.Configuration;
using Orleans.Hosting;

namespace HelloWorld.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new SiloHostBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "local";
                    options.ServiceId = "HelloWorld";
                })
                .ConfigureLogging(builder => builder.AddConsole())
                .Build();
            
            var hostTask = host.StartAsync();
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            await hostTask;
            
            await host.StopAsync();
        }
    }
}