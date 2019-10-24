using System;
using System.Threading.Tasks;
using HelloGrainLifecycle.Grains;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration.Overrides;
using Orleans.Runtime;

namespace HelloGrainLifecycle.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .ConfigureServices(services =>
                    {
                        services.AddTransient(provider => HelloComponent.Create(provider.GetRequiredService<IGrainActivationContext>()));
                    })
                .UseLocalhostClustering()
                .Build();
            
            await client.Connect();
            Console.WriteLine("Client is connected");

            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine(i);
                var grain = client.GetGrain<IHelloLifecycleGrain>(i);
                var message = await grain.SayAsync("powerumc");

                Console.WriteLine($"Grain identity: {grain.GetGrainIdentity()}");
                Console.WriteLine($"Grain primary key: {grain.GetPrimaryKey()}");
                Console.WriteLine($"Message received: {message}");
                Console.WriteLine();
            }
        }
    }
}