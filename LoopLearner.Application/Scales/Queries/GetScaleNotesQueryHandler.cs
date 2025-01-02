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
        var noteNames = GetScaleNoteNames(request.RootNote, request.ScaleType);
        var notes = await noteRepository.GetFretNotesByNamesAsync(noteNames, cancellationToken);
        var boxNotes = GroupNotesIntoBoxes(notes.ToList(), request.ScaleType);
        return Result.Success(boxNotes);
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

        var chromaticScale = Enum.GetValues(typeof(Note)).Cast<Note>().ToList();
        int rootIndex = chromaticScale.IndexOf(rootNote);
        return intervals.Select(interval => chromaticScale[(rootIndex + interval) % chromaticScale.Count]).ToList();
    }

    private Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesIntoBoxes(List<FretNote> notes, ScaleType scaleType)
    {
        var boxFretPositions = ResolveBoxFretPositions(scaleType);
        var boxNotes = new Dictionary<ScaleBoxPosition, List<FretNote>>();

        foreach (var box in boxFretPositions)
        {
            var (minFret, maxFret) = box.Value;

            boxNotes[box.Key] = notes
                .Where(n => n.Position.FretNumber >= minFret && n.Position.FretNumber <= maxFret)
                .ToList();
        }

        return boxNotes;
    }

    private Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)> ResolveBoxFretPositions(ScaleType scaleType)
    {
        return scaleType == ScaleType.PentatonicMinor
            ? new Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)>
            {
                [ScaleBoxPosition.Box1] = (0, 3),
                [ScaleBoxPosition.Box2] = (2, 5),
                [ScaleBoxPosition.Box3] = (4, 8),
                [ScaleBoxPosition.Box4] = (7, 10),
                [ScaleBoxPosition.Box5] = (9, 12)
            }
            : new Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)>
            {
                [ScaleBoxPosition.Box1] = (1, 5),
                [ScaleBoxPosition.Box2] = (4, 8),
                [ScaleBoxPosition.Box3] = (6, 10),
                [ScaleBoxPosition.Box4] = (9, 13),
                [ScaleBoxPosition.Box5] = (11, 15)
            };
    }
}