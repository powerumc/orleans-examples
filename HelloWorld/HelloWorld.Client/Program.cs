using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using HelloWorld.Grains;
using Orleans;

namespace HelloWorld.Client
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
            Console.WriteLine("Client is connected");

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Console.WriteLine($"try {j}");
                    var grain = client.GetGrain<IHelloWorldGrain>(j);
                    var message = await grain.SayAsync("powerumc");
                    Console.WriteLine($"Grain identity: {grain.GetGrainIdentity()}");
                    Console.WriteLine($"Grain primary key: {grain.GetPrimaryKey()}");
                    Console.WriteLine($"Message received: {message}");
                }
            }
        }
    }
}