using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace HelloMultiClusters.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddConfiguration(GetCommandLineConfiguration(args))
                .Build();
            
            if (configuration["ClusterId"] == null)
            {
                ShowHelp();
                return;
            }
            
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithReferences())
                .Configure<ClusterOptions>(options =>
                {
                    options.ServiceId = "HelloMultiClusters";
                    options.ClusterId = configuration["ClusterId"];
                })
                .UseAdoNetClustering(options =>
                {
                    var config = configuration.GetSection("Options:Cluster:Default");
                    options.Invariant = config["Invariant"];
                    options.ConnectionString = config["ConnectionString"];
                })
                .Configure<LoadSheddingOptions>(options =>
                {
                    options.LoadSheddingEnabled = true;
                })
                .Build();
            
            await client.Connect();

            var list = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                var grain = client.GetGrain<IHelloGrain>(i);
                list.Add(grain.SayAsync($"powerumc-{i}")
                    .ContinueWith(async task => Console.WriteLine(await task)));
            }

            await Task.WhenAll(list);
        }
        
        private static IConfigurationRoot GetCommandLineConfiguration(string[] args)
        {
            var dic = new Dictionary<string, string>
            {
                {"-c", "ClusterId"}
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
}