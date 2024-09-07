using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.SongAggregate.ValueObjects;

public record TabId : IValueObject
{
    public Guid Value { get; private set; }
    private TabId() { } 
    private TabId(Guid value)
    {
        Value = value;
    }
    public static TabId Create(Guid value) => new(value);
    public static TabId CreateNew() => new(Guid.NewGuid());
}