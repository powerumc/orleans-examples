using System;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Orleans;
using Orleans.Hosting;
using Orleans.Streams;

namespace HelloStream.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .AddSimpleMessageStreamProvider(StreamNames.PubSubProviderName)
                .Build();

            await client.Connect();
            Console.WriteLine("Connected.");
            Console.WriteLine("Send a message.");

            AssemblyLoadContext.Default.Unloading += async context => await client.Close();
            Console.CancelKeyPress += async (sender, eventArgs) => await client.Close();

            var grain = client.GetGrain<IHelloGrain>(0);
            var streamId = await grain.JoinAsync();
            var streamProvider = client.GetStreamProvider(StreamNames.PubSubProviderName);
            var stream = streamProvider.GetStream<string>(streamId, StreamNames.HelloGrainNamespace);

            await stream.SubscribeAsync(new HelloGrainStreamObserver());

            while (true)
            {
                var message = Console.ReadLine();
                await grain.SayStreamAsync(message);
            }
        }
    }

    internal class HelloGrainStreamObserver : IAsyncObserver<string>
    {

        public Task OnNextAsync(string item, StreamSequenceToken token = null)
        {
            Console.WriteLine(item);
            return Task.CompletedTask;
        }

        public Task OnCompletedAsync()
        {
            Console.WriteLine("OnCompleted");
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            Console.Error.WriteLine("OnError");
            return Task.CompletedTask;
        }
    }
}