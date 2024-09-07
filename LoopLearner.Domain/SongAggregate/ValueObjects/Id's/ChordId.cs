using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.UserAggregate.ValueObjects;

public record ChordId : IValueObject
{
    public Guid Value { get; private set; }
    private ChordId() { } 
    private ChordId(Guid value)
    {
        Value = value;
    }
    public static ChordId Create(Guid value) => new(value);
    public static ChordId CreateNew() => new(Guid.NewGuid());
}