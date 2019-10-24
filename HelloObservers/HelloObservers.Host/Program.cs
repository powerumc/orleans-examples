using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;

namespace HelloObservers.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new SiloHostBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .UseLocalhostClustering()
                .ConfigureLogging(builder => builder.AddConsole())
                .Build();
            
            await host.StartAsync();
            
            Console.WriteLine("Press any key...");
            Console.ReadLine();

            await host.StartAsync();
        }
    }
}