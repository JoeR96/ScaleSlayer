using LoopLearner.Domain.Common;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Domain.SongAggregate.Entities;
public class Chord : Entity<ChordId>
{
    private readonly IList<Note> _notes = new List<Note>();
    public NoteName RootNote { get; private set; }
    public ChordType Type { get; private set; }
    public ChordExtension Extension { get; private set; }
    public IReadOnlyList<Note> Notes => _notes.ToList();

    private Chord() { } // For ORM

    private Chord(ChordId id, NoteName rootNote, ChordType type, ChordExtension extension, IEnumerable<Note> notes)
        : base(id)
    {
        RootNote = rootNote;
        Type = type;
        Extension = extension;
        _notes = notes.ToList();
    }

    public static Chord Create(ChordId id, NoteName rootNote, ChordType type, ChordExtension extension, IEnumerable<Note> notes)
        => new(id, rootNote, type, extension, notes);

    public static Chord CreateNew(NoteName rootNote, ChordType type, ChordExtension extension, IEnumerable<Note> notes)
        => new(ChordId.CreateNew(), rootNote, type, extension, notes);

    public void AddNote(Note note)
    {
        _notes.Add(note);
    }

    public override string ToString()
    {
        string extensionPart = Extension != ChordExtension.None ? Extension.ToString() : "";
        return $"{RootNote} {Type} {extensionPart}".Trim();
    }
}
