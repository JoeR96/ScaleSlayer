using LoopLearner.Domain.Common.Interfaces;

namespace LoopLearner.Domain.SongAggregate.ValueObjects;

public record InstrumentPartId : IValueObject
{
    public Guid Value { get; private set; }
    private InstrumentPartId() { }
    private InstrumentPartId(Guid value)
    {
        Value = value;
    }
    public static InstrumentPartId Create(Guid value) => new(value);
    public static InstrumentPartId CreateNew() => new(Guid.NewGuid());
}