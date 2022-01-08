namespace EStimSystems.B2;

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