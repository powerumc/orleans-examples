using System;
using System.Runtime.Loader;
using System.Threading.Tasks;
using HelloObservers.Grains;
using Orleans;

namespace HelloObservers.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .UseLocalhostClustering()
                .Build();
            
            await client.Connect();
            AssemblyLoadContext.Default.Unloading += async context => await client.Close();
            Console.WriteLine("Connected.");
            
            var grain = client.GetGrain<IHelloNotifyGrain>(0);
            var observerRef = await client.CreateObjectReference<IHelloNotifyGrainObserver>(new HelloNotifyGrainObserver());
            await grain.SubscribeAsync(observerRef);
            
            while(true)
            {
                Console.WriteLine("Type a message.");
                var message = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(message)) continue;

                await grain.SendMessageAsync(message);
            }
        }
    }
}