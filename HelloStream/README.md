# HelloStream

A simple stream provider and grain streams.

## Server

```bash
cd HelloStream/HelloStream.Host
dotnet run
```

Results
```
Start server.
```

## Client 1

```bash
cd HelloStream/HelloStream.Client
dotnet run
```

Results
```
Connected.
Send a message.
Hello <enter>
Hello
How are you?
```

## Client 2

```bash
cd HelloStream/HelloStream.Client
dotnet run
```

Results
```
Connected.
Send a message.
Hello
How are you? <enter>
How are you?
```