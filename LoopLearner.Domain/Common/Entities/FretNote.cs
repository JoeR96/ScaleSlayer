using System.Diagnostics.CodeAnalysis;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;

namespace LoopLearner.Domain.Common.Entities;

public class FretNote : Entity<NoteId>
{
    public Note Note { get; private set; }
    public NotePosition NotePosition { get; private set; }

    [ExcludeFromCodeCoverage(Justification = "Used for EF Core")]
    private FretNote() { } 

    private FretNote(NoteId id, Note note, NotePosition notePosition) 
        : base(id)
    {
        Note = note;
        NotePosition = notePosition;
    }

    public static FretNote CreateNew(Note name, NotePosition position) 
        => new(NoteId.CreateNew(), name, position);

}