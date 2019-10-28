using System;
using System.Threading.Tasks;
using HelloCodeGen.Requests;
using HelloCodeGen.Responses;
using Orleans;

namespace HelloCodeGen.Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public Task<HelloGrainResponse> SayAsync(HelloGrainRequest request)
        {
            return Task.FromResult(new HelloGrainResponse
            {
                TraceId = request.TraceId,
                ResponseMessage = "Response " + request.Message,
                CreatedDateTime = DateTime.UtcNow
            });
        }
    }
}