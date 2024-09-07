using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.UserAggregate.ValueObjects;

public record SongId : IValueObject
{
    public Guid Value { get; private set; }
    private SongId() { } 
    private SongId(Guid value)
    {
        Value = value;
    }
    public static SongId Create(Guid value) => new(value);
    public static SongId CreateNew() => new(Guid.NewGuid());
}