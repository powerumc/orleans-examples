using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace HelloMultiClusters.Grains
{
    public class SubHelloGrain : Grain, ISubHelloGrain
    {
        public Task<string> SayAsync(string message)
        {
            Console.WriteLine($"{this.GetType().Name}: {message}");
            
            return Task.FromResult($"{message} - sub");
        }
    }
}