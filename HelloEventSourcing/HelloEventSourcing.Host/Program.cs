using System;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace HelloEventSourcing.Host
{
    class Program
    {
        static Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cancellationTokenSource.Cancel();
            AssemblyLoadContext.Default.Unloading += context => cancellationTokenSource.Cancel();

            return new HostBuilder()
                .UseOrleans(ConfigureOrleans)
                .RunConsoleAsync(cancellationTokenSource.Token);
        }

        private static void ConfigureOrleans(HostBuilderContext hostBuilderContext, ISiloBuilder siloBuilder)
        {
            siloBuilder.ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .AddStartupTask<HostStartupTask>()
                .UseLocalhostClustering()
                .AddMemoryGrainStorageAsDefault();
        }
    }

    internal class HostStartupTask : IStartupTask
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            Console.WriteLine("Server start.");
            return Task.CompletedTask;
        }
    }
}