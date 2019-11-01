using System.Threading.Tasks;
using Orleans;

namespace HelloStateless
{
    public interface IHelloStatelessLimitGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}