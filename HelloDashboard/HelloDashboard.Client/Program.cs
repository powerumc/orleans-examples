using System;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Orleans;

namespace HelloDashboard.Client
{
    class Program
    {
        static int _randomGrainKey = new Random((int) DateTime.Now.Ticks).Next(0, 1000);
        
        static async Task Main(string[] args)
        {
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
                .UseLocalhostClustering()
                .Build();

            await client.Connect();
            var manualResetEvent = new ManualResetEvent(false);
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += async (sender, eventArgs) => { await Shutdown(client, cancellationTokenSource); };
            AssemblyLoadContext.Default.Unloading += async context => { await Shutdown(client, cancellationTokenSource); };

            Console.WriteLine("Connected");
            cancellationTokenSource.Token.Register(() =>
            {
                manualResetEvent.Set();
            });
            var task = Task.Run(() => GetRandomAsync(client, cancellationTokenSource.Token), cancellationTokenSource.Token);
            
            manualResetEvent.WaitOne();
            await task;
        }

        private static async Task GetRandomAsync(IClusterClient client, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var grain = client.GetGrain<IRandomGrain>(_randomGrainKey);
                var number = await grain.GetNumberAsync();
                
                Console.WriteLine($"GetNumber: {number}");
                await Task.Delay(1000, token);
            }
        }
        
        private static async Task Shutdown(IClusterClient client, CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource.Cancel();
            await client.Close();
        }
    }
}