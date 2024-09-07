using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.UserAggregate.ValueObjects;

public record UserId : IValueObject
{
    public Guid Value { get; private set; }
    private UserId() { } 
    private UserId(Guid value)
    {
        Value = value;
    }
    public static UserId Create(Guid value) => new(value);
    public static UserId CreateNew() => new(Guid.NewGuid());
}