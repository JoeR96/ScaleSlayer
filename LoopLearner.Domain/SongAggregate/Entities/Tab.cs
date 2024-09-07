using LoopLearner.Domain.Common;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Domain.SongAggregate.Entities;

public class Tab : Entity<TabId>
{
    private readonly List<Chord> _chords = new List<Chord>();
    private readonly List<Note> _notes = new List<Note>();
    public IReadOnlyList<Chord> Chords => _chords.ToList();
    public IReadOnlyList<Note> Notes => _notes.ToList();

    private Tab() { } // For EF Core
    private Tab(TabId id)
    {
        Id = id;
    }

    public static Tab Create(TabId id) => new Tab(id);

    public void AddChord(Chord chord)
    {
        _chords.Add(chord);
    }

    public void AddNote(Note note)
    {
        _notes.Add(note);
    }

    public override string ToString()
    {
        var output = "Tab:\n";
        foreach (var note in _notes)
        {
            output += note.ToString() + "\n";
        }
        foreach (var chord in _chords)
        {
            output += chord.ToString() + "\n";
        }
        return output;
    }
}