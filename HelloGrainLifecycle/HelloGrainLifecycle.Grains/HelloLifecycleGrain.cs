using System;
using System.Threading.Tasks;
using Orleans;

namespace HelloGrainLifecycle.Grains
{
    public class HelloLifecycleGrain : Grain, IHelloLifecycleGrain
    {
        private readonly IHelloComponent _component;

        public HelloLifecycleGrain(IHelloComponent component)
        {
            _component = component;
            
            Console.WriteLine(_component.WhoAmI());
        }
        
        private int _count = 0;
        
        public Task<string> SayAsync(string name)
        {
            _count++;
            return Task.FromResult($"Hello World {name}, count = {_count}");
        }
    }
}