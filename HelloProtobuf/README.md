# HelloProtobuf

How to configure `Protobuf` serializer/deserialzer configuration for messaging.

## Common

```bash
dotnet tool install --global protobuf-net.Protogen

cd HelloProtobuf/HelloProtobuf
~/.dotnet/tools/protogen *.proto --csharp_out=.
```

## Server

```bash
cd HelloProtobuf/HelloProtobuf.Host
dotnet run
```

Results
```
Start server.
HelloGrain: None,powerumc
```

## Client

```bash
cd HelloProtobuf/HelloProtobuf.Client
dotnet run
```

Results
```
Connected.
e8fbb1a9-2333-44fb-8bb0-034e08f48566: Hello None,powerumc
```