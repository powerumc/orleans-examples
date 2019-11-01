using System.Threading.Tasks;
using Orleans;

namespace HelloEventSourcing
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task SayAsync(string message);
    }
}