using System.Threading.Tasks;
using Orleans;

namespace HelloProtobuf
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task<HelloResponse> SayAsync(HelloRequest request);
    }
}