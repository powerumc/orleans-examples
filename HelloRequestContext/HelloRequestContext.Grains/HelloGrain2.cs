using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;

namespace HelloRequestContext.Grains
{
    public class HelloGrain2 : Grain, IHelloGrain2
    {
        public async Task SayAsync()
        {
            Console.WriteLine($"Say {nameof(HelloGrain2)}");

            var traceId = RequestContext.Get("TraceId");
            Console.WriteLine($"{nameof(HelloGrain2)} TraceId: {traceId}");
            
            var grain3 = GrainFactory.GetGrain<IHelloGrain3>(0);
            await grain3.SayAsync();
        }
    }
}