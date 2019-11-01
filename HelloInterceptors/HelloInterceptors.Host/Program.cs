using System;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using HelloInterceptors.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace HelloInterceptors.Host
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
            siloBuilder
                .ConfigureServices(services =>
                    {
                        services.AddSingleton<IIncomingGrainCallFilter, GrainLoggingIncomingCallFilter>();
                        services.AddSingleton<IOutgoingGrainCallFilter, GrainLoggingOutcomingCallFilter>();
                    })
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .AddStartupTask<HostStartUp>();
            // or .AddIncomingGrainCallFilter<GrainLoggingCallFilter>();
        }
    }

    public class HostStartUp : IStartupTask
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            Console.WriteLine("Server started.");
            return Task.CompletedTask;
        }
    }
}
