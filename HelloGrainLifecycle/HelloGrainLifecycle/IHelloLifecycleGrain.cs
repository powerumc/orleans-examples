using System.Threading.Tasks;
using Orleans;

namespace HelloGrainLifecycle
{
    public interface IHelloLifecycleGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string name);
    }
}