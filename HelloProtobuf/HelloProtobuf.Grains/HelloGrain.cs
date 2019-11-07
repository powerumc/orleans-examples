using System;
using System.Linq;
using System.Threading.Tasks;
using Orleans;

namespace HelloProtobuf.Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public Task<HelloResponse> SayAsync(HelloRequest request)
        {
            var messages = request.Messages.Select(m => m.Type + "," + m.MessageValue);
            var message = string.Join("\n", messages);
            Console.WriteLine($"{GetType().Name}: {message}");
            
            return Task.FromResult(new HelloResponse
            {
                From = request.Guid,
                Message = $"Hello {message}"
            });
        }
    }
}