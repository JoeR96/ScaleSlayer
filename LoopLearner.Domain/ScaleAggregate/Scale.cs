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

    public static Scale CreateNew(Note rootNote, ScaleType scaleType)
        => new Scale(ScaleId.CreateNew(), rootNote, scaleType);

}