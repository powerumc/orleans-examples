using System.Threading.Tasks;
using Orleans;

namespace HelloRequestContext
{
    public interface IHelloGrain1 : IGrainWithIntegerKey
    {
        Task SayAsync();
    }
}