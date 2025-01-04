using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Domain.Common.Entities;

public class FretNote : Entity<NoteId>
{
    public Note Note { get; private set; }
    public NotePosition Position { get; private set; }

    private FretNote() { } 

    private FretNote(NoteId id, Note note, NotePosition position) 
        : base(id)
    {
        Note = note;
        Position = position;
    }

    public static FretNote Create(NoteId id, Note name, NotePosition position)
        => new(id, name, position);

    public static FretNote CreateNew(Note name, NotePosition position) 
        => new(NoteId.CreateNew(), name, position);

    public void UpdatePosition(NotePosition newPosition)
    {
        Position = newPosition;
    }
}