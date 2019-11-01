using System.Threading.Tasks;
using Orleans;

namespace HelloStateless
{
    public interface IHelloStatelessGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}