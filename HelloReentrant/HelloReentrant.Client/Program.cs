using System;
using System.Diagnostics;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Orleans;

namespace HelloReentrant.Client
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

            await InterleavingTest1(client);
            Console.WriteLine();
            await InterleavingTest2(client);
        }

        private static async Task InterleavingTest1(IClusterClient client)
        {
            var grain = client.GetGrain<IHelloGrain>(0);

            Console.WriteLine("Interleaving Test 1.");
            Console.WriteLine("Running SaySlow(), It will 10s.");
            await RunWithStopwatch(async () => await Task.WhenAll(grain.SaySlowAsync(), grain.SaySlowAsync()));

            Console.WriteLine("Running SayFast(), It will 5s.");
            await RunWithStopwatch(async () => await Task.WhenAll(grain.SayFastAsync(), grain.SayFastAsync()));
        }
        
        private static async Task InterleavingTest2(IClusterClient client)
        {
            var grain1 = client.GetGrain<IHelloGrain>(0);
            var grain2 = client.GetGrain<IHelloGrain>(1);

            Console.WriteLine("Interleaving Test 2.");
            Console.WriteLine("Running SaySlow(), It will 5s.");
            await RunWithStopwatch(async () => await Task.WhenAll(grain1.SaySlowAsync(), grain2.SaySlowAsync()));

            Console.WriteLine("Running SayFast(), It will 5s.");
            await RunWithStopwatch(async () => await Task.WhenAll(grain1.SayFastAsync(), grain2.SayFastAsync()));
        }

        static async Task RunWithStopwatch(Func<Task> func)
        {
            var stopwatch = Stopwatch.StartNew();
            await func();
            stopwatch.Stop();
            Console.WriteLine($"Elapsed seconds: {stopwatch.Elapsed.TotalSeconds}");
        }
    }
}