# estimsystems-b2

A E-Stim Systems B2 library for .NET (.NET Framework 4.6.1+, .NET 6.0, .NET Standard 2.0). Should work under both Linux and Windows (although, only tested under Windows).

```csharp
var factory = new B2ClientFactory();
var comPort = factory.DiscoverComPorts().Single();

using var client = factory.CreateClient(comPort);
var status = client.GetStatus();
Console.WriteLine(status);
```