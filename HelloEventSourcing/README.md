# HelloEventSourcing

About Orleans EventSourcing. See you the `Grains Streams` of pub/sub.

## Server

```bash
cd HelloEventSourcing/HelloEventSourcing.Host
dotnet run
```

Results
```
Server start.
HelloGrain: powerumc
HelloGrain UnconfirmedEvents.Count: 0
HelloGrain RaiseEvent
HelloGrain UnconfirmedEvents.Count: 1
HelloGrain: powerumc
HelloGrain UnconfirmedEvents.Count: 0
HelloGrain RaiseEvent
HelloGrain UnconfirmedEvents.Count: 1
```

## Client

```bash
cd HelloEventSourcing/HelloEventSourcing.Client
dotnet run
```

Try again.
```bash
dotnet run
```