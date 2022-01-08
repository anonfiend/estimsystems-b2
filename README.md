# estimsystems-b2

A E-Stim Systems B2 library for .NET (.NET Framework 4.6.1+, .NET 6.0, .NET Standard 2.0). Should work under both Linux and Windows (although, only tested under Windows).

```csharp
var factory = new B2ClientFactory();
var comPort = factory.DiscoverComPorts().Single();

using var client = factory.CreateClient(comPort);
var status = client.GetStatus();
Console.WriteLine(status);

# B2Response { Duration = 00:00:00.0422755, Data = B2Status { BatteryLevel = 91%, ChannelALevel = 0%, ChannelBLevel = 0%, ChannelCLevel = 50%, ChannelDLevel = 32%, Mode = SineWave, PowerSetting = High, AreChannelsJoined = False, FirmwareVersion = 2.106 } }
```