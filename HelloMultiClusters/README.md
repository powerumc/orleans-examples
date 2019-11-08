# HelloMultiClusters

How to configure multi cluster with [PostgreSQL](https://www.postgresql.org/).

#### Requirements

- Installed [Docker](https://www.docker.com/)

## Database

Launch your terminal.
```bash
cd HelloMultiClusters/provisioning
sh ./docker-run-postgresql.sh
```

And Launch you new terminal.
```bash
cd HelloMultiClusters/provisioning
sh ./docker-init-postgresql.sh
```

Created database.
- Created database `orleans`.
- Created user `powerumc`, password `powerumc`.
- Created cluster database tables.

## Server

### Server 1

```bash
cd HelloMultiClusters/HelloMultiClusters.Host
dotnet run -- -c cluster-1 -sp 11111 -gp 30000 -dp 8080 
```

Results
```
Server started.
HelloGrain: powerumc-8
HelloGrain: powerumc-1
HelloGrain: powerumc-5
SubHelloGrain: powerumc-5
HelloGrain: powerumc-0
SubHelloGrain: powerumc-7
SubHelloGrain: powerumc-0
SubHelloGrain: powerumc-6
HelloGrain: powerumc-6
HelloGrain: powerumc-4
SubHelloGrain: powerumc-7
HelloGrain: powerumc-8
SubHelloGrain: powerumc-3
HelloGrain: powerumc-1
SubHelloGrain: powerumc-9
SubHelloGrain: powerumc-5
```

### Server 2

```bash
cd HelloMultiClusters/HelloMultiClusters.Host
dotnet run -- -c cluster-1 -sp 11112 -gp 30001 
```

Results
```
Server started.
HelloGrain: powerumc-4
HelloGrain: powerumc-7
HelloGrain: powerumc-3
HelloGrain: powerumc-2
HelloGrain: powerumc-9
SubHelloGrain: powerumc-1
SubHelloGrain: powerumc-2
SubHelloGrain: powerumc-3
HelloGrain: powerumc-2
HelloGrain: powerumc-7
HelloGrain: powerumc-3
SubHelloGrain: powerumc-2
HelloGrain: powerumc-0
SubHelloGrain: powerumc-0
HelloGrain: powerumc-9
HelloGrain: powerumc-5
SubHelloGrain: powerumc-1
SubHelloGrain: powerumc-6
SubHelloGrain: powerumc-4
SubHelloGrain: powerumc-8
```

### Server 3

```bash
cd HelloMultiClusters/HelloMultiClusters.Host
dotnet run -- -c cluster-1 -sp 11113 -gp 30002 
```

Results
```
Server started.
SubHelloGrain: powerumc-8
HelloGrain: powerumc-6
SubHelloGrain: powerumc-4
SubHelloGrain: powerumc-9
```

## Client

```bash
cd HelloMultiClusters/HelloMultiClusters.Client
dotnet run -- -c cluster-1
```

Results
```
Hello powerumc-5 - sub
Hello powerumc-8 - sub
Hello powerumc-1 - sub
Hello powerumc-7 - sub
Hello powerumc-3 - sub
Hello powerumc-2 - sub
Hello powerumc-0 - sub
Hello powerumc-9 - sub
Hello powerumc-4 - sub
Hello powerumc-6 - sub
```