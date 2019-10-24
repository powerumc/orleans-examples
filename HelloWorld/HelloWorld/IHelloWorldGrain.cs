using System.Threading.Tasks;
using Orleans;

namespace HelloWorld
{
    public interface IHelloWorldGrain : IGrainWithIntegerKey
    {
        Task<string> SayAsync(string name);
    }
}