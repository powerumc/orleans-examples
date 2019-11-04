using System.Threading.Tasks;
using Orleans;

namespace HelloMultiClusters
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}