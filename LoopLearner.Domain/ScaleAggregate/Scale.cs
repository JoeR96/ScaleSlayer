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
        var intervals = GetScaleIntervals(ScaleType);
        var chromaticScale = GetChromaticScale();

        int rootIndex = GetRootIndex(chromaticScale);

        return intervals
            .Select(interval => chromaticScale[(rootIndex + interval) % chromaticScale.Length])
            .ToList();
    }

    private List<int> GetScaleIntervals(ScaleType scaleType)
    {
        var scaleIntervals = new Dictionary<ScaleType, List<int>>
        {
            { ScaleType.PentatonicMinor, new List<int> { 0, 3, 5, 7, 10 } },
            { ScaleType.Major, new List<int> { 0, 2, 4, 5, 7, 9, 11 } },
            { ScaleType.Minor, new List<int> { 0, 2, 3, 5, 7, 8, 10 } }
        };

        if (!scaleIntervals.TryGetValue(scaleType, out var intervals))
            throw new DomainException("Invalid scale type");

        return intervals;
    }

    private Note[] GetChromaticScale()
    {
        return new[]
        {
            Note.A, Note.ASharp, Note.B, Note.C, Note.CSharp, Note.D, Note.DSharp, 
            Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp
        };
    }

    private int GetRootIndex(Note[] chromaticScale)
    {
        return Array.IndexOf(chromaticScale, RootNote);
    }
    
    public Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesIntoBoxes(List<FretNote> notes)
    {
        var boxFretPositions = ResolveBoxFretPositions(); 
        return GroupNotesByBox(notes, boxFretPositions);
    }

    private Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesByBox(List<FretNote> notes, Dictionary<ScaleBoxPosition, List<FretRange>> boxFretPositions)
    {
        var boxNotes = new Dictionary<ScaleBoxPosition, List<FretNote>>();

        foreach (var box in boxFretPositions)
        {
            var scaleBox = box.Key;
            var fretRanges = box.Value;

            boxNotes[scaleBox] = notes
                .Where(n => fretRanges.Any(range => n.NotePosition.FretNumber >= range.MinFret && n.NotePosition.FretNumber <= range.MaxFret))
                .ToList();
        }

        return boxNotes;
    }

    private Dictionary<ScaleBoxPosition, List<FretRange>> ResolveBoxFretPositions(int totalFrets = 22)
    {
        var scaleIntervals = GetScaleBoxIntervals();
        var rooteNoteOffset = GetRootNoteOffset();

        var boxes = new Dictionary<ScaleBoxPosition, List<FretRange>>();

        foreach (var box in scaleIntervals)
        {
            boxes[box.Key] = ResolveFretRangesForBox(box.Value, rooteNoteOffset, totalFrets);
        }

        return boxes;
    }

    private Dictionary<ScaleBoxPosition, (int startOffset, int endOffset)> GetScaleBoxIntervals()
    {
        return new Dictionary<ScaleBoxPosition, (int startOffset, int endOffset)>
        {
            [ScaleBoxPosition.Box1] = (0, 3),
            [ScaleBoxPosition.Box2] = (2, 5),
            [ScaleBoxPosition.Box3] = (4, 8),
            [ScaleBoxPosition.Box4] = (7, 10),
            [ScaleBoxPosition.Box5] = (9, 12)
        };
    }

    private List<FretRange> ResolveFretRangesForBox((int startOffset, int endOffset) boxInterval, int rootNoteOffset, int totalFrets)
    {
        int startFret = rootNoteOffset + boxInterval.startOffset;
        int endFret = rootNoteOffset + boxInterval.endOffset;

        var fretRanges = new List<FretRange>();

        if (startFret < totalFrets && endFret <= totalFrets)
        {
            fretRanges.Add(new FretRange(startFret, endFret));
        }
        else
        {
            fretRanges.Add(new FretRange(startFret, totalFrets));
        }

        fretRanges.AddRange(HandleFretWrapAround(startFret, endFret, totalFrets));

        return fretRanges;
    }

    private List<FretRange> HandleFretWrapAround(int startFret, int endFret, int totalFrets)
    {
        var fretRanges = new List<FretRange>();

        while (startFret > 0)
        {
            startFret -= 12;
            endFret -= 12;

            if (startFret >= 0 && endFret > 0)
            {
                fretRanges.Add(new FretRange(startFret, endFret));
            }
        }

        int nextStartFret = startFret + 12;
        int nextEndFret = endFret + 12;

        while (nextStartFret < totalFrets)
        {
            fretRanges.Add(new FretRange(nextStartFret, nextEndFret));
            nextStartFret += 12;
            nextEndFret += 12;
        }

        return fretRanges;
    }

    private int GetRootNoteOffset()
    {
        var chromaticScale = GetChromaticScaleForOffset();
        return Array.IndexOf(chromaticScale, RootNote);
    }

    private Note[] GetChromaticScaleForOffset() =>
    [
        Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A, Note.ASharp, Note.B,
        Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E
    ];
}