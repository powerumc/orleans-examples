using System;
using System.Threading.Tasks;
using Orleans;

namespace HelloReentrant.Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public Task SaySlowAsync()
        {
            return Task.Delay(TimeSpan.FromSeconds(5));
        }

        public Task SayFastAsync()
        {
            return Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}