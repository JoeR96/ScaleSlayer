using LoopLearner.Domain.Common;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using System.Collections.Generic;

public class Scale : AggregateRoot<ScaleId>
{
    private readonly Dictionary<ScaleBoxPosition, List<FretNote>> _boxNotes = new();
    private readonly List<Chord> _chords = new();

    public ScaleId Id { get; private set; }
    public Note RootNote { get; private set; }
    public ScaleType ScaleType { get; private set; }
    public IReadOnlyDictionary<ScaleBoxPosition, List<FretNote>> BoxNotes => _boxNotes;
    public IReadOnlyList<Chord> Chords => _chords.ToList();

    // Private constructor for EF Core
    private Scale() { }

    private Scale(ScaleId id, Note rootNote, ScaleType scaleType)
    {
        Id = id;
        RootNote = rootNote;
        ScaleType = scaleType;
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

    // Method to add chords related to the scale
    public void AddChord(Chord chord)
    {
        _chords.Add(chord);
    }

    
    public void InitializeNotesAndChords(Dictionary<ScaleBoxPosition, List<FretNote>> boxNotes, List<Chord> chords)
    {
        foreach (var boxNote in boxNotes)
        {
            AddNotesToBox(boxNote.Key, boxNote.Value);
        }

        foreach (var chord in chords)
        {
            AddChord(chord);
        }
    }
}
