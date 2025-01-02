using LoopLearner.Domain.Common;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Domain.ScaleAggregate;

public class Scale : AggregateRoot<ScaleId>
{
    private readonly Dictionary<ScaleBoxPosition, List<FretNote>> _boxNotes = new();
    public IReadOnlyDictionary<ScaleBoxPosition, List<FretNote>> BoxNotes => _boxNotes;

    // Private constructor for EF Core
    private Scale() { }

    private Scale(ScaleId id, Note rootNote, ScaleType scaleType)
    {
    }

    public static Scale Create(ScaleId id, Note rootNote, ScaleType scaleType)
        => new Scale(id, rootNote, scaleType);

    public static Scale CreateNew(Note rootNote, ScaleType scaleType)
        => new Scale(ScaleId.CreateNew(), rootNote, scaleType);

    // Method to add notes to a specific box position
    public void AddNotesToBox(ScaleBoxPosition boxPosition, List<FretNote> notes)
    {
        if (_boxNotes.ContainsKey(boxPosition))
        {
            _boxNotes[boxPosition].AddRange(notes);
        }
        else
        {
            _boxNotes[boxPosition] = new List<FretNote>(notes);
        }
    }
}