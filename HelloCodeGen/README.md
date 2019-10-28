# Hello Code Generation

Runtime code generation with `Microsoft.Orleans.OrleansCodeGenerator`.

## Server

```bash
cd HelloCodeGen.Host
dotnet run
```

Results
```
info: Orleans.CodeGenerator.RoslynCodeGenerator[0]
      Generating code for assemblies: 
info: RuntimeCodeGen[0]
      Runtime code generation for assemblies  HelloCodeGen.Host, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null HelloCodeGen.Grains, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.Runtime.Abstractions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null HelloCodeGen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.CodeGeneration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.Runtime, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null OrleansProviders, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null took 4850 milliseconds
Press any key.
```

## Client

```bash
cd HelloCodeGen.Client
dotnet run
```

Results
```
info: Orleans.CodeGenerator.RoslynCodeGenerator[0]
      Generating code for assemblies: 
info: RuntimeCodeGen[0]
      Runtime code generation for assemblies  HelloCodeGen.Host, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null HelloCodeGen.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null HelloCodeGen.Grains, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.Runtime.Abstractions, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null HelloCodeGen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.CodeGeneration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.Runtime, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null Orleans.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null OrleansProviders, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null took 2565 milliseconds
# All grains
  - HelloCodeGen.Grains.HelloGrain
  - Orleans.Streams.PubSubRendezvousGrain
  - Orleans.Runtime.Versions.VersionStoreGrain
  - Orleans.Runtime.ReminderService.GrainBasedReminderTable
  - Orleans.Runtime.Development.DevelopmentLeaseProviderGrain
  - Orleans.Runtime.Management.ManagementGrain
  - Orleans.Storage.MemoryStorageGrain
  - Orleans.Providers.MemoryStreamQueueGrain
--------------
TraceId: 976312e9-afd0-468b-b1fd-3b6a5b52468c
Response Message: Response HelloWorld
Created DateTime: 10/28/2019 06:11:26
```