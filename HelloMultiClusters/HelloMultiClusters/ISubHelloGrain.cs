using System.Threading.Tasks;
using Orleans;

namespace HelloMultiClusters
{
    public interface ISubHelloGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}