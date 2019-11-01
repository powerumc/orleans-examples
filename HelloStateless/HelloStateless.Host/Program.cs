using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace HelloStateless.Host
{
    class Program
    {
        static Task Main(string[] args)
        {
            var configuration = GetCommandLineConfiguration(args);
            if (configuration["ClusterId"] == null)
            {
                ShowHelp();
                return Task.CompletedTask;
            }
            
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) => cancellationTokenSource.Cancel();
            AssemblyLoadContext.Default.Unloading += context => cancellationTokenSource.Cancel();

            return new HostBuilder()
                .UseOrleans(ConfigureOrleans)
                .ConfigureHostConfiguration(builder => builder.AddConfiguration(configuration))
                .RunConsoleAsync(cancellationTokenSource.Token);
        }

        private static void ConfigureOrleans(HostBuilderContext hostBuilder, ISiloBuilder siloBuilder)
        {
            siloBuilder
                .AddStartupTask<HostStartupTask>()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ServiceId = "HelloStateless";
                    options.ClusterId = hostBuilder.Configuration["ClusterId"];
                })
                .Configure<EndpointOptions>(options =>
                {
                    options.AdvertisedIPAddress = IPAddress.Loopback;
                    options.SiloPort = int.Parse(hostBuilder.Configuration["SiloPort"]);
                    options.GatewayPort = int.Parse(hostBuilder.Configuration["GatewayPort"]);
                });
        }

        private static IConfigurationRoot GetCommandLineConfiguration(string[] args)
        {
            var dic = new Dictionary<string, string>
            {
                {"-c", "ClusterId"},
                {"-sp", "SiloPort"},
                {"-gp", "GatewayPort"}
            };
            
            return new ConfigurationBuilder()
                .AddCommandLine(args, dic)
                .Build();
        }

        private static void ShowHelp()
        {
            Console.WriteLine($@"Help:
-c <string>: Cluster Id

ex) {Process.GetCurrentProcess().ProcessName} -c Cluster-1
ex) dotnet run -- -c Cluster-1
");
        }
    }

    internal class HostStartupTask : IStartupTask
    {
        private readonly IConfiguration _configuration;

        public HostStartupTask(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Task Execute(CancellationToken cancellationToken)
        {
            
            Console.WriteLine("Server started.");
            Console.WriteLine($"ClusterId: {_configuration["ClusterId"]}");
            return Task.CompletedTask;
        }
    }
}