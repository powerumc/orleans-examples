using System;
using System.Threading;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;

namespace HelloGrainLifecycle.Grains
{
    public class HelloComponent : IHelloComponent, ILifecycleParticipant<IGrainLifecycle>
    {
        public static IHelloComponent Create(IGrainActivationContext context)
        {
            var component = new HelloComponent();
            component.Participate(context.ObservableLifecycle);
            return component;
        }

        public string WhoAmI()
        {
            return $"I'm {typeof(HelloComponent).Name}";
        }
        
        public void Participate(IGrainLifecycle lifecycle)
        {
            lifecycle.Subscribe<HelloComponent>(GrainLifecycleStage.Activate, OnActivate);
            lifecycle.Subscribe<HelloComponent>(GrainLifecycleStage.First, OnFirst);
            lifecycle.Subscribe<HelloComponent>(GrainLifecycleStage.Last, OnLast);
            lifecycle.Subscribe<HelloComponent>(GrainLifecycleStage.SetupState, OnSetupState);
        }

        private static Task OnActivate(CancellationToken arg)
        {
            Console.WriteLine("OnActivate");
            return Task.CompletedTask;
        }

        private static Task OnFirst(CancellationToken arg)
        {
            Console.WriteLine("OnFirst");
            return Task.CompletedTask;
        }

        private static Task OnLast(CancellationToken arg)
        {
            Console.WriteLine("OnLast");
            return Task.CompletedTask;
        }

        private static Task OnSetupState(CancellationToken arg)
        {
            Console.WriteLine("OnSetupState");
            return Task.CompletedTask;
        }
    }
}