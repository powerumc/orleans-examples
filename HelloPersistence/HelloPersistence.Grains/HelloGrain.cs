using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;

namespace HelloPersistence.Grains
{
    public class HelloGrain : Grain<HelloGrainState>, IHelloGrain
    {
        public override Task OnActivateAsync()
        {
            return ReadStateAsync();
        }

        public async Task<string> SayAsync(string message)
        {
            await ReadStateAsync();
            Console.WriteLine(string.IsNullOrEmpty(State?.Message)
                ? $"{nameof(HelloGrain)}: State message is empty"
                : $"{nameof(HelloGrain)}: State message: {State.Message}");

            var newMessage = "Hello " + message;

            State ??= new HelloGrainState();
            State.Message = newMessage;
            await WriteStateAsync();

            Console.WriteLine($"{nameof(HelloGrain)}: {newMessage}");
            return newMessage;
        }
    }
}