using System;
using System.Threading.Tasks;
using Orleans;

namespace HelloInterceptors.Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        public Task<string> SayAsync(string message)
        {
            Console.WriteLine(message);

            GrainFactory.GetGrain<IHelloGrain2>(0).Something(message);
            
            var newMessage = $"Hello {message}";
            return Task.FromResult(newMessage);
        }
    }
}