using LoopLearner.Domain.Common;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Domain.SongAggregate.Entities;

public class Tab : Entity<TabId>
{
    private readonly List<Chord> _chords = new List<Chord>();
    private readonly List<FretNote> _notes = new List<FretNote>();
    public IReadOnlyList<Chord> Chords => _chords.ToList();
    public IReadOnlyList<FretNote> Notes => _notes.ToList();

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

    public void AddNote(FretNote fretNote)
    {
        _notes.Add(fretNote);
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