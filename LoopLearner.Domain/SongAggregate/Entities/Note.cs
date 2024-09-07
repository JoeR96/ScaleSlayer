using LoopLearner.Domain.Common;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Domain.SongAggregate.Entities;

public class Note : Entity<NoteId>
{
    public NoteName NoteName { get; private set; }
    public NotePosition Position { get; private set; }

    private Note() { } // For ORM

    private Note(NoteId id, NoteName noteName, NotePosition position) 
        : base(id)
    {
        NoteName = noteName;
        Position = position;
    }

    public static Note Create(NoteId id, NoteName name, NotePosition position)
        => new(id, name, position);

    public static Note CreateNew(NoteName name, NotePosition position) 
        => new(NoteId.CreateNew(), name, position);

    public void UpdatePosition(NotePosition newPosition)
    {
        Position = newPosition;
    }

    public override string ToString() => $"{NoteName} on {Position}";
}