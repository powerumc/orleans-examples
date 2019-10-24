using System.Threading.Tasks;
using Orleans;

namespace HelloWorld.Grains
{
    public class HelloWorldGrain : Grain, IHelloWorldGrain
    {
        private int _count = 0;
        
        public Task<string> SayAsync(string name)
        {
            _count++;
            return Task.FromResult($"Hello World {name}, count = {_count}");
        }
    }
}