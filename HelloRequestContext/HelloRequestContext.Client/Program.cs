using System;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Orleans;

namespace HelloRequestContext.Client
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

            Console.CancelKeyPress += async (sender, eventArgs) => await ShutdownAsync(client);
            AssemblyLoadContext.Default.Unloading += async context => await ShutdownAsync(client);
            Console.WriteLine("Connected.");

            var grain = client.GetGrain<IHelloGrain1>(0);
            await grain.SayAsync();
        }

        private static async Task ShutdownAsync(IClusterClient client)
        {
            await client.Close();
        }
    }
}