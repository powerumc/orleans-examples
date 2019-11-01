# HelloInterceptors

Incoming and Outgoing interception the `Grain`.

## Server

```bash
cd HelloInterceptors/HelloInterceptors.Host
dotnet run
```

Results
```
Server started.
GrainLoggingIncomingCallFilter Grain Before Invoke
GrainLoggingIncomingCallFilter Grain Name: HelloInterceptors.Grains.HelloGrain
GrainLoggingIncomingCallFilter Grain Interface Type: HelloInterceptors.IHelloGrain
GrainLoggingIncomingCallFilter Grain Implementation Type: HelloInterceptors.Grains.HelloGrain
GrainLoggingIncomingCallFilter Grain PrimaryKey: 0
GrainLoggingIncomingCallFilter Grain Method Arguments: powerumc
powerumc
GrainLoggingOutcomingCallFilter Grain Before Invoke
GrainLoggingOutcomingCallFilter Grain Name: HelloInterceptors.OrleansCodeGenHelloGrain2Reference
GrainLoggingOutcomingCallFilter Grain Interface Type: HelloInterceptors.IHelloGrain2
GrainLoggingOutcomingCallFilter Grain PrimaryKey: 0
GrainLoggingOutcomingCallFilter Grain Method Arguments: powerumc
GrainLoggingIncomingCallFilter Grain After Invoke
GrainLoggingIncomingCallFilter Grain Result: Hello powerumc
GrainLoggingIncomingCallFilter Grain Before Invoke
GrainLoggingIncomingCallFilter Grain Name: HelloInterceptors.Grains.HelloGrain2
GrainLoggingIncomingCallFilter Grain Interface Type: HelloInterceptors.IHelloGrain2
GrainLoggingIncomingCallFilter Grain Implementation Type: HelloInterceptors.Grains.HelloGrain2
GrainLoggingIncomingCallFilter Grain PrimaryKey: 0
GrainLoggingIncomingCallFilter Grain Method Arguments: powerumc
GrainLoggingIncomingCallFilter Grain After Invoke
GrainLoggingIncomingCallFilter Grain Result: 
GrainLoggingOutcomingCallFilter Grain After Invoke
GrainLoggingOutcomingCallFilter Grain Result:
```

## Client

```bash
cd HelloInterceptors/HelloInterceptors.Client
dotnet run
```

Results
```
Connected
GrainLoggingOutcomingCallFilter Grain Before Invoke
GrainLoggingOutcomingCallFilter Grain Name: HelloInterceptors.OrleansCodeGenHelloGrainReference
GrainLoggingOutcomingCallFilter Grain Interface Type: HelloInterceptors.IHelloGrain
GrainLoggingOutcomingCallFilter Grain PrimaryKey: 0
GrainLoggingOutcomingCallFilter Grain Method Arguments: powerumc
GrainLoggingOutcomingCallFilter Grain After Invoke
GrainLoggingOutcomingCallFilter Grain Result: Hello powerumc
Hello powerumc
```