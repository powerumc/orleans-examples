
using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;

namespace HelloRequestContext.Grains
{
    public class HelloGrain3 : Grain, IHelloGrain3
    {
        public Task SayAsync()
        {
            Console.WriteLine($"Say {nameof(HelloGrain3)}");
            
            var traceId = RequestContext.Get("TraceId");
            Console.WriteLine($"{nameof(HelloGrain2)} TraceId: {traceId}");
            return Task.CompletedTask;
        }
    }
}