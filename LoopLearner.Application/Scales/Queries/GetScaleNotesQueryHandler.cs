using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Application.Scales.Responses;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public class GetScaleNotesQueryHandler(INoteRepository noteRepository)
    : IRequestHandler<GetScaleNotesQuery, Result<ScaleNotesResponse>>
{
    public async Task<Result<ScaleNotesResponse>> Handle(GetScaleNotesQuery request, CancellationToken cancellationToken)
    {
        int rootNoteOffset = GetRootNoteOffset(request.RootNote);
        var noteNames = GetScaleNoteNames(request.RootNote, request.ScaleType);
        var notes = await noteRepository.GetFretNotesByNamesAsync(noteNames, cancellationToken);
        var boxNotes = GroupNotesIntoBoxes(notes.ToList(), rootNoteOffset);

        return Result.Success(MapToDto(boxNotes));
    }

    private List<Note> GetScaleNoteNames(Note rootNote, ScaleType scaleType)
    {
        var scaleIntervals = new Dictionary<ScaleType, List<int>>
        {
            { ScaleType.PentatonicMinor, [0, 3, 5, 7, 10] },
            { ScaleType.Major, [0, 2, 4, 5, 7, 9, 11] },
            { ScaleType.Minor, [0, 2, 3, 5, 7, 8, 10] }
        };

        if (!scaleIntervals.TryGetValue(scaleType, out var intervals))
            throw new ArgumentException("Invalid scale type", nameof(scaleType));

        var chromaticScale = new[]
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


    private Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesIntoBoxes(List<FretNote> notes, int rootNote)
    {
        var boxFretPositions = ResolveBoxFretPositions(rootNote); 
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


    private Dictionary<ScaleBoxPosition, List<FretRange>> ResolveBoxFretPositions(int rootNote, int totalFrets = 22)
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


    private int GetRootNoteOffset(Note rootNote)
    {
        var chromaticScale = new[]
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
    
    private ScaleNotesResponse MapToDto(Dictionary<ScaleBoxPosition, List<FretNote>> scaleNotes)
    {
        var mappedScaleNotes = new Dictionary<ScaleBoxPosition, List<FretNoteDto>>();

        foreach (var scaleNote in scaleNotes)
        {
            var mappedFretNotes = scaleNote.Value.Select(fretNote => new FretNoteDto(
                fretNote.Note, 
                new NotePositionDto(fretNote.NotePosition.StringNumber, fretNote.NotePosition.FretNumber)
            )).ToList();

            mappedScaleNotes.Add(scaleNote.Key, mappedFretNotes);
        }

        return new ScaleNotesResponse(mappedScaleNotes);
    }
} 
