using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace HelloStream
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task<Guid> JoinAsync();
        Task SayStreamAsync(string message);
    }
}