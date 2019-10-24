using System;

namespace HelloObservers.Grains
{
    public class HelloNotifyGrainObserver : IHelloNotifyGrainObserver
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"ReceiveMessage: {message}");
        }
    }
}