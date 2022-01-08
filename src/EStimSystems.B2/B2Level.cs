namespace EStimSystems.B2;

public record B2Level(int Percentage, string RawValue)
{
    public override string ToString()
    {
        return $"{Percentage}%";
    }
}