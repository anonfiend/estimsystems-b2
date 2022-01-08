# estimsystems-b2
A E-Stim Systems B2 library for .NET.

```csharp
var factory = new B2ClientFactory();
var comPort = factory.DiscoverComPorts().Single();

using var client = factory.CreateClient(comPort);
var status = client.GetStatus();
Console.WriteLine(status);
```