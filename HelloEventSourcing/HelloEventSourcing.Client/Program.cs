using System;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Orleans;
using Orleans.Hosting;

namespace HelloEventSourcing.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .Build();

            await client.Connect();
            AssemblyLoadContext.Default.Unloading += async context => await client.Close();

            var grain = client.GetGrain<IHelloGrain>(0);
            await grain.SayAsync("powerumc");
        }
    }
}