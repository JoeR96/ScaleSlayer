using LoopLearner.Domain.Common;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Domain.SongAggregate.Entities;
public class Chord : Entity<ChordId>
{
    private readonly IList<FretNote> _notes = new List<FretNote>();
    public Note RootNote { get; private set; }
    public ChordType ChordType { get; private set; }
    public ChordExtension ChordExtension { get; private set; }
    public IReadOnlyList<FretNote> Notes => _notes.ToList();

    private Chord() { } // For ORM

    private Chord(ChordId id, Note rootNote, ChordType chordType, ChordExtension chordExtension, IEnumerable<FretNote> notes)
        : base(id)
    {
        RootNote = rootNote;
        ChordType = chordType;
        ChordExtension = chordExtension;
        _notes = notes.ToList();
    }

    public static Chord Create(ChordId id, Note rootNote, ChordType type, ChordExtension extension, IEnumerable<FretNote> notes)
        => new(id, rootNote, type, extension, notes);

    public static Chord CreateNew(Note rootNote, ChordType type, ChordExtension extension, IEnumerable<FretNote> notes)
        => new(ChordId.CreateNew(), rootNote, type, extension, notes);

    public void AddNote(FretNote fretNote)
    {
        _notes.Add(fretNote);
    }

    public override string ToString()
    {
        string extensionPart = ChordExtension != ChordExtension.None ? ChordExtension.ToString() : "";
        return $"{RootNote} {ChordType} {extensionPart}".Trim();
    }
}
