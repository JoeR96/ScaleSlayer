using System.Diagnostics.CodeAnalysis;
using ScaleSlayer.Domain.Common.Interfaces;

namespace ScaleSlayer.Domain.UserAggregate.ValueObjects;

public record UserId : IValueObject
{
    public Guid Value { get; private set; }
    
    [ExcludeFromCodeCoverage(Justification = "Used for EF Core")]
    private UserId() { } 
    private UserId(Guid value)
    {
        Value = value;
    }
    public static UserId Create(Guid value) => new(value);
    public static UserId CreateNew() => new(Guid.NewGuid());
}