namespace EStimSystems.B2;

public record B2Status(B2Level BatteryLevel,
                       B2Level ChannelALevel,
                       B2Level ChannelBLevel,
                       B2Level ChannelCLevel,
                       B2Level ChannelDLevel,
                       B2Mode CurrentMode,
                       B2PowerSetting PowerSetting,
                       bool AreChannelsJoined,
                       string FirmwareVersion);