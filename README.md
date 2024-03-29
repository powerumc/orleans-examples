# Orleans examples

[Orleans](https://github.com/dotnet/orleans) is virtual actor model implementation.

## Grain

### Basic
- [HelloWorld](./HelloWorld/) - Understand basically `Grain`.
- [HelloGrainLifecycle](./HelloGrainLifecycle/) - Lifecycle shows how to inject objects and manage their lifecycle.
- [HelloObservers](./HelloObservers/) - Implements `Pub/Sub` pattern through `Grain` with `IGrainObserver`.
- [HelloReentrant](./HelloReentrant/) - Concurrently calls the `Grain`.
- [HelloDashboard](./HelloDashboard/) - Sample `Orleans Dashboard`.
- [HelloRequestContext](./HelloRequestContext/) - `RequestContext` is propagated to all child `Grains`.
- [HelloCodeGen](./HelloCodeGen/) - Runtime code generation with `Microsoft.Orleans.OrleansCodeGenerator`.

### Advanced
- [HelloPersistence](./HelloPersistence/) - How to configure persistently `Grain` storage with [PostgreSQL](https://www.postgresql.org/).
- [HelloEventSourcing](./HelloEventSourcing/) - About Orleans EventSourcing. See you the `Grains Streams` of pub/sub.
- [HelloInterceptors](./HelloInterceptors/) - Incoming and Outgoing interception the `Grain`.
- [HelloStream](./HelloStream/) - A simple stream provider and grain streams.

## Hosting
- [HelloMultiClusters](./HelloMultiClusters) - How to configure multi cluster with [PostgreSQL](https://www.postgresql.org/).
- [HelloProtobuf](./HelloProtobuf) - How to configure `Protobuf` serializer/deserialzer configuration for messaging.