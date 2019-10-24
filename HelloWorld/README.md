# HelloWorld

Understand basically `Grain`. `Grain` is has Identity and Primary key.

`Grain` creates an object for each primary key.


## Server

```bash
cd HelloWorld.Host
dotnet run
```

## Client
```bash
cd HelloWorld.Client
dotnet run
```

Results
```
Client is connected
try 0
Grain identity: *grn/92D7D64A/00000000
Grain primary key: 00000000-0000-0000-0000-000000000000
Message received: Hello World powerumc, count = 1
try 1
Grain identity: *grn/92D7D64A/00000001
Grain primary key: 00000000-0000-0000-0100-000000000000
Message received: Hello World powerumc, count = 1
try 2
Grain identity: *grn/92D7D64A/00000002
Grain primary key: 00000000-0000-0000-0200-000000000000
Message received: Hello World powerumc, count = 1
try 0
Grain identity: *grn/92D7D64A/00000000
Grain primary key: 00000000-0000-0000-0000-000000000000
Message received: Hello World powerumc, count = 2
try 1
Grain identity: *grn/92D7D64A/00000001
Grain primary key: 00000000-0000-0000-0100-000000000000
Message received: Hello World powerumc, count = 2
try 2
Grain identity: *grn/92D7D64A/00000002
Grain primary key: 00000000-0000-0000-0200-000000000000
Message received: Hello World powerumc, count = 2
try 0
Grain identity: *grn/92D7D64A/00000000
Grain primary key: 00000000-0000-0000-0000-000000000000
Message received: Hello World powerumc, count = 3
try 1
Grain identity: *grn/92D7D64A/00000001
Grain primary key: 00000000-0000-0000-0100-000000000000
Message received: Hello World powerumc, count = 3
try 2
Grain identity: *grn/92D7D64A/00000002
Grain primary key: 00000000-0000-0000-0200-000000000000
Message received: Hello World powerumc, count = 3
```