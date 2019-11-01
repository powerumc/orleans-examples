using System.Threading.Tasks;
using Orleans;

namespace HelloInterceptors
{
    [GrainLogging]
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}