# HelloObservers

Implements `Pub/Sub` pattern through `Grain` with `IGrainObserver`. 

## Server

```bash
cd HelloObservers.Host
dotnet run
```

Results
```
Subscribe observer: HelloObservers.OrleansCodeGenHelloNotifyGrainObserverReference
Subscribe observer: HelloObservers.OrleansCodeGenHelloNotifyGrainObserverReference

Send message: Hello powerumc
Send message: Hello Hanji
```

## Client 1

```bash
cd HelloObservers.Host
dotnet run
```

Results
```
Connected.
Type a message.
Hello powerumc
Type a message.
ReceiveMessage: Hello powerumc
ReceiveMessage: Hello Hanji
```

## Client 2

```bash
cd HelloObservers.Host
dotnet run
```

Results
```
Connected.
Type a message.
ReceiveMessage: Hello powerumc
Hello Hanji
ReceiveMessage: Hello Hanji
Type a message.
```