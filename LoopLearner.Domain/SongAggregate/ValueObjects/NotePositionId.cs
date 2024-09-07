using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.UserAggregate.ValueObjects;

public record NotePositionId : IValueObject
{
    public Guid Value { get; private set; }
    private NotePositionId() { } 
    private NotePositionId(Guid value)
    {
        Value = value;
    }
    public static NotePositionId Create(Guid value) => new(value);
    public static NotePositionId CreateNew() => new(Guid.NewGuid());
}