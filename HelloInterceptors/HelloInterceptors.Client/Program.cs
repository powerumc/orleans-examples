using System;
using System.Threading.Tasks;
using HelloInterceptors.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;

namespace HelloInterceptors.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ClientBuilder()
                .ConfigureServices(services =>
                    {
                        services.AddSingleton<IOutgoingGrainCallFilter, GrainLoggingOutcomingCallFilter>();
                    })
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .Build();

            await client.Connect();
            Console.WriteLine("Connected");

            var grain = client.GetGrain<IHelloGrain>(0);
            var message = await grain.SayAsync("powerumc");
            Console.WriteLine(message);
            
            await client.Close();
        }
    }
}
