using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace HelloStream.Grains
{
    public class HelloGrain : Grain, IHelloGrain
    {
        private IAsyncStream<string> stream;

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(StreamNames.PubSubProviderName);
            stream = streamProvider.GetStream<string>(Guid.NewGuid(), StreamNames.HelloGrainNamespace);
            return base.OnActivateAsync();
        }

        public Task<Guid> JoinAsync()
        {
            return Task.FromResult(stream.Guid);
        }
        
        public async Task SayStreamAsync(string message)
        {
            await stream.OnNextAsync(message);
        }
    }
}