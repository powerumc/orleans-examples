using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace HelloReentrant
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task SaySlowAsync();
        
        [AlwaysInterleave]
        Task SayFastAsync();
    }
}