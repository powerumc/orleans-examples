using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Orleans;
using Orleans.Hosting;
using Orleans.Providers.Streams.Generator;
using Orleans.Runtime;

namespace HelloStream.Host
{
    class Program
    {
        static Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            return new HostBuilder()
                .UseOrleans(ConfigureOrleans)
                .RunConsoleAsync(cancellationTokenSource.Token);
        }

        private static void ConfigureOrleans(ISiloBuilder siloBuilder)
        {
            siloBuilder
                .AddStartupTask<HostStartUpTask>()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .AddSimpleMessageStreamProvider(StreamNames.PubSubProviderName)
                .AddMemoryGrainStorage(StreamNames.PubSubStorageName);
        }
    }

    internal class HostStartUpTask : IStartupTask
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start server.");
            return Task.CompletedTask;
        }
    }
}