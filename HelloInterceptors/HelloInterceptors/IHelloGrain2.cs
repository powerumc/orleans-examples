using System.Threading.Tasks;
using Orleans;

namespace HelloInterceptors
{
    [GrainLogging]
    public interface IHelloGrain2 : IGrainWithIntegerKey
    {
        Task Something(string message);
    }
}