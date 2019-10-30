using System.Threading.Tasks;
using Orleans;

namespace HelloPersistence
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}