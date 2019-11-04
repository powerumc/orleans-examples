using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace HelloMultiClusters.Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public async Task<string> SayAsync(string message)
        {
            Console.WriteLine($"{GetType().Name}: {message}");
            
            var grain = GrainFactory.GetGrain<ISubHelloGrain>(GrainReference.GetPrimaryKeyLong());
            var result = await grain.SayAsync(message);

            await Task.Delay(1000);

            return $"Hello {result}";
        }
    }
}