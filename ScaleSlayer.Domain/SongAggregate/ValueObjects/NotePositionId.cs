using System.Diagnostics.CodeAnalysis;
using ScaleSlayer.Domain.Common.Interfaces;

namespace ScaleSlayer.Domain.SongAggregate.ValueObjects;

public record NotePositionId : IValueObject
{
    public Guid Value { get; private set; }
    
    [ExcludeFromCodeCoverage(Justification = "Used for EF Core")]
    private NotePositionId() { } 
    private NotePositionId(Guid value)
    {
        Value = value;
    }
    public static NotePositionId Create(Guid value) => new(value);
    public static NotePositionId CreateNew() => new(Guid.NewGuid());
}