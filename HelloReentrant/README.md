# HelloReentrant

Concurrently calls the `Grain`.

## Server

```bash
cd HelloReentrant.Host
dotnet run
```

# Client

```bash
cd HelloReentrant.Client
dotnet run
```

Results
```
Interleaving Test 1.
Running SaySlow(), It will 10s.
Elapsed seconds: 10.0113629
Running SayFast(), It will 5s.
Elapsed seconds: 5.0058324999999995

Interleaving Test 2.
Running SaySlow(), It will 10s.
Elapsed seconds: 5.006541299999999
Running SayFast(), It will 5s.
Elapsed seconds: 5.0071612
```