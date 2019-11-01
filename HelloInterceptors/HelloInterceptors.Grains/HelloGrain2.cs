using System.Threading.Tasks;
using Orleans;

namespace HelloInterceptors.Grains
{
    public class HelloGrain2 : Grain, IHelloGrain2
    {
        public Task Something(string message)
        {
            return Task.CompletedTask;
        }
    }
}