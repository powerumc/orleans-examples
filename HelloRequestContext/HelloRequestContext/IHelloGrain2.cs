using System.Threading.Tasks;
using Orleans;

namespace HelloRequestContext
{
    public interface IHelloGrain2 : IGrainWithIntegerKey
    {
        Task SayAsync();
    }
}