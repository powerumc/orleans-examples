using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace HelloStateless.Grains
{
    public class HelloStatefulGrain : Grain, IHelloStatefulGrain
    {
        private int _count = 0;
        
        public Task<string> SayAsync(string message)
        {
            _count++;
            Console.WriteLine($"{this.GetType().Name} Count: {_count}");
            return Task.FromResult($"Hello {message}");
        }
    }
    
    [StatelessWorker]
    public class HelloStatelessGrain : Grain, IHelloStatelessGrain
    {
        private int _count;

        public Task<string> SayAsync(string message)
        {
            _count++;
            Console.WriteLine($"{this.GetType().Name} Count: {_count}");
            return Task.FromResult($"Hello {message}");
        }
    }
    
    [StatelessWorker(2)]
    public class HelloStatelessLimitGrain : Grain, IHelloStatelessLimitGrain
    {
        private int _count;

        public Task<string> SayAsync(string message)
        {
            _count++;
            Console.WriteLine($"{this.GetType().Name} Count: {_count}");
            return Task.FromResult($"Hello {message}");
        }
    }
}