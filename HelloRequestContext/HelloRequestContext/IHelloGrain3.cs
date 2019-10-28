using System.Threading.Tasks;
using Orleans;

namespace HelloRequestContext
{
    public interface IHelloGrain3 : IGrainWithIntegerKey
    {
        Task SayAsync();
    }
}