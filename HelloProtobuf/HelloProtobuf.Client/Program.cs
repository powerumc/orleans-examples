using System;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Serialization.ProtobufNet;

namespace HelloProtobuf.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .Configure<SerializationProviderOptions>(options =>
                {
                    options.SerializationProviders.Add(typeof(ProtobufNetSerializer));
                })
                .Build();

            await client.Connect();
            Console.WriteLine("Connected.");

            Console.CancelKeyPress += async (sender, eventArgs) => await client.Close();
            AssemblyLoadContext.Default.Unloading += async context => await client.Close();

            var grain = client.GetGrain<IHelloGrain>(0);
            var response = await grain.SayAsync(new HelloRequest
            {
                Guid = Guid.NewGuid().ToString(),
                Messages = {new Message {Type = Message.MessageType.None, MessageValue = "powerumc"}}
            });
            
            Console.WriteLine($"{response.From}: {response.Message}");
        }
    }
}