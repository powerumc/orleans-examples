using System;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Orleans;
using Orleans.Hosting;

namespace HelloRequestContext.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new SiloHostBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .UseLocalhostClustering()
                .Build();

            await host.StartAsync();
            Console.CancelKeyPress += async (sender, eventArgs) => await ShutdownAsync(host);
            AssemblyLoadContext.Default.Unloading += async context => await ShutdownAsync(host);

            Console.WriteLine("Press any key.");
            Console.ReadLine();
        }

        private static async Task ShutdownAsync(ISiloHost host)
        {
            await host.StopAsync();
        }
    }
}