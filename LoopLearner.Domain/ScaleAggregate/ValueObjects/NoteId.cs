using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.UserAggregate.ValueObjects;

public record NoteId : IValueObject
{
    public Guid Value { get; private set; }
    private NoteId() { } 
    private NoteId(Guid value)
    {
        Value = value;
    }
    public static NoteId Create(Guid value) => new(value);
    public static NoteId CreateNew() => new(Guid.NewGuid());
}