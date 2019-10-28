using System.Threading.Tasks;
using HelloCodeGen.Requests;
using HelloCodeGen.Responses;
using Orleans;

namespace HelloCodeGen
{
    public interface IHelloGrain : IGrainWithIntegerKey
    {
        Task<HelloGrainResponse> SayAsync(HelloGrainRequest request);
    }
}