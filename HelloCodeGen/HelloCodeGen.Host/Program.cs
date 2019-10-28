using System;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;

namespace HelloCodeGen.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var codeGenLogger = LoggerFactory.Create(builder => builder.AddConsole());
            var host = new SiloHostBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration(codeGenLogger))
                .UseLocalhostClustering()
                .Build();

            Console.CancelKeyPress += async (sender, eventArgs) => await ShutdownAsync(host);
            AssemblyLoadContext.Default.Unloading += async context => await ShutdownAsync(host);

            await host.StartAsync();

            foreach (var grain in host.Services.GetServices<IGrain>())
            {
                Console.WriteLine($"Grain: {grain.GetGrainIdentity()}");
            }
            
            Console.WriteLine("Press any key.");
            Console.ReadLine();
        }

        private static async Task ShutdownAsync(ISiloHost host)
        {
            await host.StopAsync();
        }
    }
}