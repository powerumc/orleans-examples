# Orleans examples

[Orleans](https://github.com/dotnet/orleans) is virtual actor model implementation.

## Grain Basic

- [HelloWorld](./HelloWorld/README.md) - Understand basically `Grain`.
- [HelloGrainLifecycle](./HelloGrainLifecycle/README.md) - Lifecycle shows how to inject objects and manage their lifecycle.
- [HelloObservers](./HelloObservers/README.md) - Implements `Pub/Sub` pattern through `Grain` with `IGrainObserver`.
- [HelloReentrant](./HelloReentrant/README.md) - Concurrently calls the `Grain`.
- [HelloDashboard](./HelloDashboard/README.md) - Sample `Orleans Dashboard`.
- [HelloRequestContext](./HelloRequestContext/README.md) - `RequestContext` is propagated to all child `Grains`.