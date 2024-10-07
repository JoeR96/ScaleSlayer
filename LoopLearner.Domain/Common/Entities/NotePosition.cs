using LoopLearner.Domain.Common;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Domain.SongAggregate.ValueObjects;

public class NotePosition : Entity<NotePositionId>
{
    public int StringNumber { get; private set; }
    public int FretNumber { get; private set; }

    private NotePosition() { } // For ORM

    private NotePosition(NotePositionId id, int stringNumber, int fretNumber)
        : base(id)
    {
        StringNumber = stringNumber;
        FretNumber = fretNumber;
    }

    public static NotePosition Create(NotePositionId id, int stringNumber, int fretNumber)
        => new(id, stringNumber, fretNumber);   
    public static NotePosition CreateNew(int stringNumber, int fretNumber)
        => new(NotePositionId.CreateNew(), stringNumber, fretNumber);

    public void UpdatePosition(int stringNumber, int fretNumber)
    {
        StringNumber = stringNumber;
        FretNumber = fretNumber;
    }

    public override string ToString()
    {
        return $"String {StringNumber}, Fret {FretNumber}";
    }
}