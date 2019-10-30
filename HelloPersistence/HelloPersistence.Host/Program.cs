using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace HelloPersistence.Host
{
    class Program
    {
        static Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cancellationTokenSource.Cancel();

            return new HostBuilder()
                .ConfigureAppConfiguration(ConfigureApp)
                .UseOrleans(ConfigureOrleans)
                .RunConsoleAsync(cancellationTokenSource.Token);
        }

        private static void ConfigureApp(HostBuilderContext hostBuilderContext, IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
        }

        private static void ConfigureOrleans(HostBuilderContext hostBuilderContext, ISiloBuilder siloBuilder)
        {
            siloBuilder
                .ConfigureServices((context, services) =>
                {
                    services.Configure<AdoNetGrainStorageOptions>("Default",
                        context.Configuration.GetSection("Options:Persistence:Default"));
                })
                .AddStartupTask<HostStartup>()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .AddAdoNetGrainStorage("Default");
        }
    }

    internal class HostStartup : IStartupTask
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            Console.WriteLine("Press any key.");
            return Task.CompletedTask;
        }
    }
}