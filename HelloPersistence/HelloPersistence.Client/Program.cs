using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Orleans;
using Orleans.Hosting;

namespace HelloPersistence.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetCommandLineConfiguration(args);
            if (configuration["IsHelp"] != null)
            {
                ShowHelp();
                return;
            }
            
            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithCodeGeneration())
                .UseLocalhostClustering()
                .Build();

            await client.Connect();
            Console.CancelKeyPress += async (sender, eventArgs) => await ShutdownAsync(client);
            AssemblyLoadContext.Default.Unloading += async context => await ShutdownAsync(client);
            
            Console.WriteLine("Connected.");

            if (!long.TryParse(configuration["GrainId"], out var id)) id = 0;
            var message = configuration["GrainMessage"] ?? "powerumc";

            var grain1 = client.GetGrain<IHelloGrain>(id);
            Console.WriteLine(await grain1.SayAsync(message));

            var grain2 = client.GetGrain<ICustomProfileHelloGrain>(id);
            Console.WriteLine(await grain2.SayAsync(message));
        }

        private static IConfigurationRoot GetCommandLineConfiguration(string[] args)
        {
            var commandLineDictionary = new Dictionary<string, string>
            {
                ["-h"] = "IsHelp",
                ["-i"] = "GrainId",
                ["-m"] = "GrainMessage"
            };
            
            return new ConfigurationBuilder()
                .AddCommandLine(args, commandLineDictionary)
                .Build();
        }

        private static void ShowHelp()
        {
            Console.WriteLine($@"Help:
-i : Grain Id (number)
-m : Grain Message (string)

ex) {Process.GetCurrentProcess().ProcessName} -i 0 -m Hello
ex) dotnet run -- -i 0 -m Hello
");
        }

        private static Task ShutdownAsync(IClusterClient client)
        {
            return client.Close();
        }
    }
}