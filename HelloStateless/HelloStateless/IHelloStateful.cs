using System.Threading.Tasks;
using Orleans;

namespace HelloStateless
{
    public interface IHelloStatefulGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}