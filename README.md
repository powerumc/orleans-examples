# Orleans examples

[Orleans](https://github.com/dotnet/orleans) is virtual actor model implementation.

## Grain Basic

- [HelloWorld](./HelloWorld/) - Understand basically `Grain`.
- [HelloGrainLifecycle](./HelloGrainLifecycle/) - Lifecycle shows how to inject objects and manage their lifecycle.
- [HelloObservers](./HelloObservers/) - Implements `Pub/Sub` pattern through `Grain` with `IGrainObserver`.
- [HelloReentrant](./HelloReentrant/) - Concurrently calls the `Grain`.
- [HelloDashboard](./HelloDashboard/) - Sample `Orleans Dashboard`.
- [HelloRequestContext](./HelloRequestContext/) - `RequestContext` is propagated to all child `Grains`.
- [HelloCodeGen](./HelloCodeGen/) - Runtime code generation with `Microsoft.Orleans.OrleansCodeGenerator`.