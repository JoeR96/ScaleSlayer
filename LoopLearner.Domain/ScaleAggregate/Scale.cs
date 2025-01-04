using System.Diagnostics.CodeAnalysis;
using LoopLearner.Domain.Common;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.Common.Exceptions;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;

namespace LoopLearner.Domain.ScaleAggregate;

public class Scale : AggregateRoot<ScaleId>
{
    public Note RootNote { get; private set; }
    public ScaleType ScaleType { get; private set; }
    
    // Private constructor for EF Core
    [ExcludeFromCodeCoverage(Justification = "Used for EF Core")]
    private Scale() { }

    private Scale(ScaleId id, Note rootNote, ScaleType scaleType)
    {
        RootNote = rootNote;
        ScaleType = scaleType;
    }

    public static Scale CreateNew(Note rootNote, ScaleType scaleType)
        => new Scale(ScaleId.CreateNew(), rootNote, scaleType);

    public List<Note> GetScaleNoteNames()
    {
        var scaleIntervals = new Dictionary<ScaleType, List<int>>
        {
            { ScaleType.PentatonicMinor, [0, 3, 5, 7, 10] },
            { ScaleType.Major, [0, 2, 4, 5, 7, 9, 11] },
            { ScaleType.Minor, [0, 2, 3, 5, 7, 8, 10] }
        };

        if (!scaleIntervals.TryGetValue(ScaleType, out var intervals))
            throw new DomainException("Invalid scale type");

        var chromaticScale = new[]
        {
            Note.A, Note.ASharp, Note.B, Note.C, Note.CSharp, Note.D, Note.DSharp, 
            Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp
        };

        int rootIndex = Array.IndexOf(chromaticScale, RootNote);

        var notes = intervals
            .Select(interval => chromaticScale[(rootIndex + interval) % chromaticScale.Length])
            .ToList();

        return notes;
    }
    
    public Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesIntoBoxes(List<FretNote> notes)
    {
        var boxFretPositions = ResolveBoxFretPositions(); 
        var boxNotes = new Dictionary<ScaleBoxPosition, List<FretNote>>();

        foreach (var box in boxFretPositions)
        {
            var scaleBox = box.Key; 
            var fretRanges = box.Value; 

            boxNotes[scaleBox] = notes
                .Where(n => fretRanges.Any(range => 
                    n.NotePosition.FretNumber >= range.MinFret && n.NotePosition.FretNumber <= range.MaxFret))
                .ToList();
        }

        return boxNotes;
    }

    private Dictionary<ScaleBoxPosition, List<FretRange>> ResolveBoxFretPositions(int totalFrets = 22)
    {
        var scaleIntervals = new Dictionary<ScaleBoxPosition, (int startOffset, int endOffset)>
        {
            [ScaleBoxPosition.Box1] = (0, 3),
            [ScaleBoxPosition.Box2] = (2, 5),
            [ScaleBoxPosition.Box3] = (4, 8),
            [ScaleBoxPosition.Box4] = (7, 10),
            [ScaleBoxPosition.Box5] = (9, 12)
        };

        var boxes = new Dictionary<ScaleBoxPosition, List<FretRange>>();

        var rooteNoteOffset = GetRootNoteOffset();
        foreach (var box in scaleIntervals)
        {
            int startFret = rooteNoteOffset + box.Value.startOffset;
            int endFret = rooteNoteOffset + box.Value.endOffset;

            if (startFret < totalFrets && endFret <= totalFrets)
            {
                boxes[box.Key] = [new FretRange(startFret, endFret)];
            }
            else
            {
                boxes[box.Key] = [new FretRange(startFret, 22)];

            }

            while (startFret > 0)
            {
                startFret -= 12;
                endFret -= 12;

                if (startFret >= 0 && endFret > 0)
                {
                    boxes[box.Key].Add(new FretRange(startFret, endFret));
                }
            }

            int nextStartFret = rooteNoteOffset + box.Value.startOffset + 12;
            int nextEndFret = rooteNoteOffset + box.Value.endOffset + 12;

            while (nextStartFret < totalFrets)
            {
                boxes[box.Key].Add(new FretRange(nextStartFret, nextEndFret));
                nextStartFret += 12;
                nextEndFret += 12;
            }
        }

        return boxes;
    }

    private int GetRootNoteOffset()
    {
        var chromaticScale = new[]
        {
            Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A, Note.ASharp, Note.B,
            Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E
        };

        int offset = Array.IndexOf(chromaticScale, RootNote);
    
        if (offset == -1)
        {
            throw new DomainException($"Invalid root note: {RootNote}");
        }

        return offset;
    }
}