using System.Threading.Tasks;
using Orleans;

namespace HelloObservers
{
    public interface IHelloNotifyGrain : IGrainWithIntegerKey
    {
        Task SubscribeAsync(IHelloNotifyGrainObserver observer);
        Task UnsubscribeAsync(IHelloNotifyGrainObserver observer);
        Task SendMessageAsync(string message);
    }
}