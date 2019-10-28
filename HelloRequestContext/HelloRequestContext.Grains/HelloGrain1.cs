using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;

namespace HelloRequestContext.Grains
{
    public class HelloGrain1 : Grain, IHelloGrain1
    {
        public async Task SayAsync()
        {
            Console.WriteLine($"Say {nameof(HelloGrain1)}");
            
            RequestContext.Set("TraceId", Guid.NewGuid());
            var grain2 = GrainFactory.GetGrain<IHelloGrain2>(0);
            await grain2.SayAsync();
        }
    }
}