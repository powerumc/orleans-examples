using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Internal;
using Orleans.Serialization;

namespace HelloObservers.Grains
{
    public class HelloNotifyGrain : Grain, IHelloNotifyGrain
    {
        private GrainObserverManager<IHelloNotifyGrainObserver> _observerManager;

        public override Task OnActivateAsync()
        {
            this._observerManager = new GrainObserverManager<IHelloNotifyGrainObserver>();
            return base.OnActivateAsync();
        }

        public Task SubscribeAsync(IHelloNotifyGrainObserver observer)
        {
            Console.WriteLine($"Subscribe observer: {observer.GetType().FullName}");
            
            if (!_observerManager.IsSubscribed(observer))
            {
                _observerManager.Subscribe(observer);
            }

            return Task.CompletedTask;
        }

        public Task UnsubscribeAsync(IHelloNotifyGrainObserver observer)
        {
            Console.WriteLine($"Unsubscribe observer: {observer.GetType().FullName}");
            
            if (_observerManager.IsSubscribed(observer))
            {
                _observerManager.Unsubscribe(observer);
            }
            
            return Task.CompletedTask;
        }

        public Task SendMessageAsync(string message)
        {
            Console.WriteLine($"Send message: {message}");
            
            _observerManager.Notify(observer => observer.ReceiveMessage(message));
            return Task.CompletedTask;
        }
    }
}
