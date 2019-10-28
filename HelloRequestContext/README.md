# Hello RequestContext

`RequestContext` is propagated to all child `Grains`.

## Server

```bash
cd HelloRequestContext.Host
dotnet run
```

Results
```
Press any key.
Say HelloGrain1
Say HelloGrain2
HelloGrain2 TraceId: 2e52506c-c9a1-4c68-99f9-81000563fee8
Say HelloGrain3
HelloGrain2 TraceId: 2e52506c-c9a1-4c68-99f9-81000563fee8
```

## Client

```bash
cd HelloRequestContext.Client
dotnet run
```