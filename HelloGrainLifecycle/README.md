# HelloGrainLifecycle

Lifecycle shows how to inject objects and manage their lifecycle.

## Server

```bash
cd HelloGrainLifecycle.Host
dotnet run
```

Results
```
I'm HelloComponent
OnFirst
OnSetupState
OnActivate
OnLast
```

## Client

```bash
cd HelloGrainLifecycle.Client
dotnet run
```

Results
```
Client is connected
0
Grain identity: *grn/D019F652/00000000
Grain primary key: 00000000-0000-0000-0000-000000000000
Message received: Hello World powerumc, count = 1

1
Grain identity: *grn/D019F652/00000001
Grain primary key: 00000000-0000-0000-0100-000000000000
Message received: Hello World powerumc, count = 1

2
Grain identity: *grn/D019F652/00000002
Grain primary key: 00000000-0000-0000-0200-000000000000
Message received: Hello World powerumc, count = 1

```