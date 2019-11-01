using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;

namespace HelloStateless.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetCommandLineConfiguration(args);
            if (configuration["ClusterId"] == null)
            {
                ShowHelp();
                return;
            }
            
            var client = new ClientBuilder()
                .UseLocalhostClustering(new[] {30000, 30001})
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithReferences())
                .Configure<ClusterOptions>(options =>
                {
                    options.ServiceId = "HelloStateless";
                    options.ClusterId = configuration["ClusterId"];
                })
                .Build();
            
            await client.Connect();
            Console.WriteLine($"Connected to {configuration["ClusterId"]}");

            Console.WriteLine(await client.GetGrain<IHelloStatefulGrain>(0).SayAsync("Stateful powerumc"));
            Console.WriteLine(await client.GetGrain<IHelloStatelessGrain>(0).SayAsync("Stateless powerumc"));
            Console.WriteLine(await client.GetGrain<IHelloStatelessLimitGrain>(0).SayAsync("StatelessLimit powerumc"));
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