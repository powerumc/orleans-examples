# HelloPersistence

How to configure persistently `Grain` storage with [PostgreSQL](https://www.postgresql.org/). 

#### Requirements

- Installed [Docker](https://www.docker.com/) 

## Database

Launch your terminal.
```bash
cd HelloPersistence/provisioning
sh ./docker-run-postgresql.sh
```

And Launch you new terminal.
```bash
cd HelloPersistence/provisioning
sh ./docker-init-postgresql.sh
```

Created database.
- Created database `orleans`.
- Created user `powerumc`, password `powerumc`.
- Created `Orleans` database tables.

## Server

```bash
cd HelloPersistence/HelloPersistence.Host
dotnet run
```

Results
```
Press any key.
HelloGrain: State message is empty
HelloGrain: Hello AAA
CustomProfileHelloGrain: ETag: 
CustomProfileHelloGrain: State.Message: 
CustomProfileHelloGrain: Hello AAA

HelloGrain: State message: Hello AAA
HelloGrain: Hello BBB
CustomProfileHelloGrain: ETag: 1
CustomProfileHelloGrain: State.Message: Hello AAA
CustomProfileHelloGrain: Hello BBB

HelloGrain: State message is empty
HelloGrain: Hello CCC
CustomProfileHelloGrain: ETag: 
CustomProfileHelloGrain: State.Message: 
CustomProfileHelloGrain: Hello CCC
```

## Client

```bash
cd HelloPersistence/HelloPersistence.Client
```

Results

`dotnet run -- -i 0 -m AAA`
```
Connected.
Hello AAA
Hello AAA
```

`dotnet run -- -i 0 -m BBB`
```
Connected.
Hello BBB
Hello BBB
```

`dotnet run -- -i 1 -m CCC`
```
Connected.
Hello CCC
Hello CCC
```

## Database

```bash
docker exec -it orleans-postgres psql -U powerumc -w -d orleans
```

```sql
SELECT * FROM storage;
```

Results
```
grainidhash grainidn0   grainidn1   graintypehash   graintypestring grainidextensionstring  serviceid   payloadbinary   payloadxml  payloadjson modifiedon  version
1447026491	0	0	706493367	HelloPersistence.Grains.HelloGrain		dev			{"$id":"1","$type":"HelloPersistence.Grains.HelloGrainState, HelloPersistence.Grains","Message":"Hello BBB"}	2019-10-30 02:34:49.551469	2
1447026491	0	0	-1399706923	HelloPersistence.Grains.CustomProfileHelloGrain,HelloPersistence.Grains.customState		dev			{"$id":"1","$type":"HelloPersistence.Grains.HelloGrainState, HelloPersistence.Grains","Message":"Hello BBB"}	2019-10-30 02:34:49.567491	2
1808487381	0	1	706493367	HelloPersistence.Grains.HelloGrain		dev			{"$id":"1","$type":"HelloPersistence.Grains.HelloGrainState, HelloPersistence.Grains","Message":"Hello CCC"}	2019-10-30 02:36:35.581163	1
1808487381	0	1	-1399706923	HelloPersistence.Grains.CustomProfileHelloGrain,HelloPersistence.Grains.customState		dev			{"$id":"1","$type":"HelloPersistence.Grains.HelloGrainState, HelloPersistence.Grains","Message":"Hello CCC"}	2019-10-30 02:36:35.613770	1

```