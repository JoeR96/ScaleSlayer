using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public class GetScaleNotesQueryHandler(INoteRepository noteRepository)
    : IRequestHandler<GetScaleNotesQuery, Result<Dictionary<ScaleBoxPosition, List<FretNote>>>>
{
    public async Task<Result<Dictionary<ScaleBoxPosition, List<FretNote>>>> Handle(GetScaleNotesQuery request, CancellationToken cancellationToken)
    {
        int rootNoteOffset = GetRootNoteOffset(request.RootNote);
        var noteNames = GetScaleNoteNames(request.RootNote, request.ScaleType);
        var notes = await noteRepository.GetFretNotesByNamesAsync(noteNames, cancellationToken);
        var boxNotes = GroupNotesIntoBoxes(notes.ToList(), request.ScaleType, rootNoteOffset);
        
        return Result.Success(boxNotes);
    }

    private List<Note> GetScaleNoteNames(Note rootNote, ScaleType scaleType)
    {
        var scaleIntervals = new Dictionary<ScaleType, List<int>>
        {
            { ScaleType.PentatonicMinor, new List<int> { 0, 3, 5, 7, 10 } },
            { ScaleType.Major, new List<int> { 0, 2, 4, 5, 7, 9, 11 } },
            { ScaleType.Minor, new List<int> { 0, 2, 3, 5, 7, 8, 10 } }
        };

        if (!scaleIntervals.TryGetValue(scaleType, out var intervals))
            throw new ArgumentException("Invalid scale type", nameof(scaleType));

        var chromaticScale = new Note[]
        {
            Note.A, Note.ASharp, Note.B, Note.C, Note.CSharp, Note.D, Note.DSharp, 
            Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp
        };

        int rootIndex = Array.IndexOf(chromaticScale, rootNote);

        var notes = intervals
            .Select(interval => chromaticScale[(rootIndex + interval) % chromaticScale.Length])
            .ToList();

        return notes;
    }


    private Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesIntoBoxes(List<FretNote> notes, ScaleType scaleType, int rootNote)
    {
        var boxFretPositions = ResolveBoxFretPositions(scaleType, rootNote); 
        var boxNotes = new Dictionary<ScaleBoxPosition, List<FretNote>>();

        foreach (var box in boxFretPositions)
        {
            var scaleBox = box.Key; 
            var fretRanges = box.Value; 

            boxNotes[scaleBox] = notes
                .Where(n => fretRanges.Any(range => 
                    n.Position.FretNumber >= range.MinFret && n.Position.FretNumber <= range.MaxFret))
                .ToList();
        }

        return boxNotes;
    }


    private Dictionary<ScaleBoxPosition, List<FretRange>> ResolveBoxFretPositions(ScaleType scaleType, int rootNote, int totalFrets = 22)
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

        foreach (var box in scaleIntervals)
        {
            int startFret = rootNote + box.Value.startOffset;
            int endFret = rootNote + box.Value.endOffset;

            if (startFret < totalFrets && endFret <= totalFrets)
            {
                boxes[box.Key] = new List<FretRange> { new FretRange(startFret, endFret) };
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

            int nextStartFret = rootNote + box.Value.startOffset + 12;
            int nextEndFret = rootNote + box.Value.endOffset + 12;

            while (nextStartFret < totalFrets)
            {
                boxes[box.Key].Add(new FretRange(nextStartFret, nextEndFret));
                nextStartFret += 12;
                nextEndFret += 12;
            }
        }

        return boxes;
    }


    public int GetRootNoteOffset(Note rootNote)
    {
        var chromaticScale = new Note[]
        {
            Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A, Note.ASharp, Note.B,
            Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E
        };

        int offset = Array.IndexOf(chromaticScale, rootNote);
    
        if (offset == -1)
        {
            throw new ArgumentException($"Invalid root note: {rootNote}");
        }

        return offset;
    }
}

public class FretRange
{
    public int MinFret { get; set; }
    public int MaxFret { get; set; }

    public FretRange(int minFret, int maxFret)
    {
        MinFret = minFret;
        MaxFret = maxFret;
    }

    public override string ToString() => $"{MinFret}-{MaxFret}";
}
