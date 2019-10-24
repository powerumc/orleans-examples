using Orleans;

namespace HelloObservers
{
    public interface IHelloNotifyGrainObserver : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
}