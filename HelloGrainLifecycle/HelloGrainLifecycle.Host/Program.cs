using System;
using System.Threading.Tasks;
using HelloGrainLifecycle.Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;

namespace HelloGrainLifecycle.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new SiloHostBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .UseLocalhostClustering()
                .ConfigureLogging(builder => builder.AddConsole())
                .ConfigureServices(services =>
                {
                    services.AddTransient(provider => HelloComponent.Create(provider.GetRequiredService<IGrainActivationContext>()));
                })
                .Build();

            await host.StartAsync();
            Console.WriteLine("Press any key...");
            
            Console.ReadLine();
            
            await host.StopAsync();
        }
    }
}