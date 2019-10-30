using System.Threading.Tasks;
using Orleans;

namespace HelloPersistence
{
    public interface ICustomProfileHelloGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string message);
    }
}