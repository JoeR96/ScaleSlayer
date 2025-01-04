namespace ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

public record FretRange
{
    public int MinFret { get; set; }
    public int MaxFret { get; set; }

    public FretRange(int minFret, int maxFret)
    {
        MinFret = minFret;
        MaxFret = maxFret;
    }
}