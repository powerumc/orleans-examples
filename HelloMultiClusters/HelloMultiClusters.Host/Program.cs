using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace HelloMultiClusters.Host
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
            return new HostBuilder()
                .ConfigureAppConfiguration(ConfigureApp)
                .ConfigureHostConfiguration(builder => builder.AddConfiguration(configuration))
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
                .AddStartupTask<HostStartupTask>()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .Configure<MultiClusterOptions>(options =>
                {
                    options.HasMultiClusterNetwork = true;
                    options.BackgroundGossipInterval = TimeSpan.FromSeconds(10);
                    options.UseGlobalSingleInstanceByDefault = false;
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ServiceId = "HelloMultiClusters";
                    options.ClusterId = hostBuilderContext.Configuration["ClusterId"];
                })
                .Configure<EndpointOptions>(options =>
                {
                    options.AdvertisedIPAddress = IPAddress.Loopback;
                    options.SiloPort = int.Parse(hostBuilderContext.Configuration["SiloPort"]);
                    options.GatewayPort = int.Parse(hostBuilderContext.Configuration["GatewayPort"]);
                })
                .UseAdoNetClustering(options =>
                {
                    var config = hostBuilderContext.Configuration.GetSection("Options:Cluster:Default");
                    options.Invariant = config["Invariant"];
                    options.ConnectionString = config["ConnectionString"];
                })
                .Configure<GrainCollectionOptions>(options =>
                {
                    options.CollectionAge = TimeSpan.Parse("00:01:01"); 
                });

            if (hostBuilderContext.Configuration["DashboardPort"] != null)
            {
                siloBuilder.UseDashboard(options =>
                {
                    options.Host = "*";
                    options.HostSelf = true;
                    options.Port = int.Parse(hostBuilderContext.Configuration["DashboardPort"]);
                    options.CounterUpdateIntervalMs = 1000;
                });
            }
        }

        private static IConfigurationRoot GetCommandLineConfiguration(string[] args)
        {
            var dic = new Dictionary<string, string>
            {
                {"-c", "ClusterId"},
                {"-sp", "SiloPort"},
                {"-gp", "GatewayPort"},
                {"-dp", "DashboardPort"}
            };
            
            return new ConfigurationBuilder()
                .AddCommandLine(args, dic)
                .Build();
        }

        private static void ShowHelp()
        {
            Console.WriteLine($@"Help:
-c <string>: Cluster Id
-sp <number>: Silo port
-gp <number>: Gateway Port
-dp <number>: Dashboard port

ex) {Process.GetCurrentProcess().ProcessName} -c Cluster-1 -sp 11111 -gp 30000
ex) dotnet run -- -c Cluster-1 -sp 11111 -gp 30000
");
        }
    }

    internal class HostStartupTask : IStartupTask
    {
        public Task Execute(CancellationToken cancellationToken)
        {
            Console.WriteLine("Server started.");
            return Task.CompletedTask;
        }
    }
}