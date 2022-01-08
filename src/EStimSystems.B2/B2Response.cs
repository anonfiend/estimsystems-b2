using System;

namespace EStimSystems.B2;

public record B2Response(TimeSpan Duration);

public record B2Response<T>(TimeSpan Duration, T Data) : B2Response(Duration);

public record B2Status(B2Level BatteryLevel,
                       B2Level ChannelALevel,
                       B2Level ChannelBLevel,
                       B2Level ChannelCLevel,
                       B2Level ChannelDLevel,
                       B2Mode Mode,
                       B2PowerSetting PowerSetting,
                       bool AreChannelsJoined,
                       string FirmwareVersion);

public record B2Snapshot(int ChannelALevel,
                         int ChannelBLevel,
                         int ChannelCLevel,
                         int ChannelDLevel,
                         B2Mode Mode,
                         B2PowerSetting PowerSetting,
                         bool AreChannelsJoined);

public enum B2PowerSetting
{
    Low,
    High
}

public record B2Level(int Percentage, string RawValue)
{
    public override string ToString()
    {
        return $"{Percentage}%";
    }
}

public enum B2Mode
{
    /// <summary>
    /// Pulse
    /// </summary>
    Pulse = 0,

    /// <summary>
    /// Bounce
    /// </summary>
    Alternating = 1,

    /// <summary>
    /// Continuous
    /// </summary>
    Continuous = 2,

    /// <summary>
    /// A Split
    /// </summary>
    APattern = 3,

    /// <summary>
    /// B Split
    /// </summary>
    BPattern = 4,

    /// <summary>
    /// Wave
    /// </summary>
    AsymmetricPowerRamp = 5,

    /// <summary>
    /// Waterfall
    /// </summary>
    SymmetricPowerRamp = 6,

    /// <summary>
    /// Squeeze
    /// </summary>
    FrequencyRamp = 7,

    /// <summary>
    /// Milk
    /// </summary>
    AlternativeFrequencyRamp = 8,

    /// <summary>
    /// Throb
    /// </summary>
    SawWave = 9,

    /// <summary>
    /// Thrust
    /// </summary>
    SineWave = 10,

    /// <summary>
    /// Random
    /// </summary>
    Random = 11,

    /// <summary>
    /// Step
    /// </summary>
    Step = 12,

    /// <summary>
    /// Training
    /// </summary>
    Jump = 13,
}