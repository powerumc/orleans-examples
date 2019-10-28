using System;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Orleans;
using Orleans.Hosting;
using OrleansDashboard;

namespace HelloDashboard.Host
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new SiloHostBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .UseLocalhostClustering()
                .UseDashboard(ConfigureDashboard)
                .Build();

            await host.StartAsync();
            Console.CancelKeyPress += async (sender, eventArgs) => { await ShutdownAsync(host); };
            AssemblyLoadContext.Default.Unloading += async context => { await ShutdownAsync(host); };
            
            Console.WriteLine("Press any key.");
            Console.WriteLine("Open http://localhost:8080 for dashboard");
            Console.ReadLine();
        }

        private static async Task ShutdownAsync(ISiloHost host)
        {
            await host.StopAsync();
        }

        private static void ConfigureDashboard(DashboardOptions options)
        {
            options.Host = "*";
            options.Port = 8080;
            options.HostSelf = true;
            options.CounterUpdateIntervalMs = 1000;
        }
    }
}