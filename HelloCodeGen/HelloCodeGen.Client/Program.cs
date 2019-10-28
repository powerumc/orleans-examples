using System;
using System.Reflection;
using System.Threading.Tasks;
using HelloCodeGen;
using HelloCodeGen.Grains;
using HelloCodeGen.Requests;
using HelloCodeGen.Responses;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.CodeGeneration;
using Orleans.Hosting;
using Orleans.Runtime;

[assembly: KnownAssembly(typeof(IHelloGrain))]

namespace HelloCodeGen.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var codeGenLogger = LoggerFactory.Create(builder => builder.AddConsole());
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(Assembly.GetExecutingAssembly()).WithCodeGeneration(codeGenLogger))
                .UseLocalhostClustering()
                .Build();
            
            await client.Connect();
            
            Console.WriteLine("# All grains");
            foreach(var activeGrain in await client.GetGrain<IManagementGrain>(0).GetActiveGrainTypes())
            {
                Console.WriteLine($"  - {activeGrain}");
            }
            Console.WriteLine("--------------");
            
            var grain = client.GetGrain<IHelloGrain>(0);
            var response = await grain.SayAsync(new HelloGrainRequest
            {
                TraceId = Guid.NewGuid(),
                Message = "HelloWorld"
            });
            
            Console.WriteLine($"TraceId: {response.TraceId}");
            Console.WriteLine($"Response Message: {response.ResponseMessage}");
            Console.WriteLine($"Created DateTime: {response.CreatedDateTime}");

            await client.Close();
        }
    }
}