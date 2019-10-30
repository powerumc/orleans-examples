using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;

namespace HelloPersistence.Grains
{
    public class CustomProfileHelloGrain : Grain, ICustomProfileHelloGrain
    {
        private readonly IPersistentState<HelloGrainState> _helloState;

        public CustomProfileHelloGrain([PersistentState("customState", "Default")] IPersistentState<HelloGrainState> helloState)
        {
            _helloState = helloState;
        }
        
        public async Task<string> SayAsync(string message)
        {
            await _helloState.ReadStateAsync();
            Console.WriteLine($"{nameof(CustomProfileHelloGrain)}: ETag: {_helloState.Etag}");
            Console.WriteLine($"{nameof(CustomProfileHelloGrain)}: State.Message: {_helloState.State.Message}");
            
            var newMessage = "Hello " + message;
            
            _helloState.State.Message = newMessage;
            await _helloState.WriteStateAsync();

            Console.WriteLine($"{nameof(CustomProfileHelloGrain)}: {newMessage}");
            return newMessage;
        }
    }
}