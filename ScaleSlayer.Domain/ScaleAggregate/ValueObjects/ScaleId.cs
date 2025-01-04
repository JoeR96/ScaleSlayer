using ScaleSlayer.Domain.Common.Interfaces;

namespace ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

public record ScaleId : IValueObject
{
    public Guid Value { get; private set; }
    private ScaleId() { }
    private ScaleId(Guid value)
    {
        Value = value;
    }

    public static ScaleId CreateNew() => new(Guid.NewGuid());
}