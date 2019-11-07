using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using Orleans.Serialization.ProtobufNet;

namespace HelloProtobuf.Host
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
                .AddStartupTask<HostStartupTask>()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .Configure<SerializationProviderOptions>(options =>
                {
                    options.SerializationProviders.Add(typeof(ProtobufNetSerializer));
                });
        }
    }

    internal class HostStartupTask : IStartupTask
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start server.");
            return Task.CompletedTask;
        }
    }
}