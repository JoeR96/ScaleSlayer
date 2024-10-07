using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries
{
    public class GetScaleNotesQueryHandler : IRequestHandler<GetScaleNotesQuery, Result<Dictionary<ScaleBoxPosition, List<FretNote>>>>
    {
        private readonly INoteRepository _noteRepository;

        // Define note offsets for standard tuning
        private readonly Dictionary<Note, int> noteOffsets = new Dictionary<Note, int>
        {
            { Note.E, 0 }, // Open E
            { Note.F, 1 }, // 1st fret
            { Note.FSharp, 2 }, // 2nd fret
            { Note.G, 3 }, // 3rd fret
            { Note.GSharp, 4 }, // 4th fret
            { Note.A, 5 }, // 5th fret
            { Note.ASharp, 6 }, // 6th fret
            { Note.B, 7 }, // 7th fret
            { Note.C, 8 }, // 8th fret
            { Note.CSharp, 9 }, // 9th fret
            { Note.D, 10 }, // 10th fret
            { Note.DSharp, 11 } // 11th fret
        };

        public GetScaleNotesQueryHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Result<Dictionary<ScaleBoxPosition, List<FretNote>>>> Handle(GetScaleNotesQuery request, CancellationToken cancellationToken)
        {
            var noteNames = GetScaleNoteNames(request.RootNote, request.ScaleType);
            var notes = await _noteRepository.GetFretNotesByNamesAsync(noteNames, cancellationToken);
            var boxNotes = GroupNotesIntoBoxes(notes.ToList(), request.RootNote);
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

            var chromaticScale = Enum.GetValues(typeof(Note)).Cast<Note>().ToList();
            int rootIndex = chromaticScale.IndexOf(rootNote);
            return intervals.Select(interval =>
            {
                int noteIndex = (rootIndex + interval) % chromaticScale.Count;
                return chromaticScale[noteIndex];
            }).ToList();
        }

        private Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesIntoBoxes(List<FretNote> notes, Note rootNote)
{
    var boxFretPositions = ResolveBoxFretPositions();
    var boxNotes = new Dictionary<ScaleBoxPosition, List<FretNote>>();

    int rootOffset = noteOffsets[rootNote];

    foreach (var box in boxFretPositions)
    {
        var (minFret, maxFret) = box.Value;

        int adjustedMinFret = WrapFretNumber(minFret + rootOffset);
        int adjustedMaxFret = WrapFretNumber(maxFret + rootOffset);

        boxNotes[box.Key] = notes
            .Where(n =>
                (n.Position.FretNumber >= adjustedMinFret && n.Position.FretNumber <= adjustedMaxFret) ||
                (n.Position.FretNumber >= WrapFretNumber(adjustedMinFret + 12) && n.Position.FretNumber <= WrapFretNumber(adjustedMaxFret + 12))
            )
            .ToList();
    }

    return boxNotes;
}

// Helper method to wrap fret numbers
private int WrapFretNumber(int fretNumber)
{
    return fretNumber > 22 ? fretNumber - 23 : fretNumber;
}
        private Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)> ResolveBoxFretPositions()
        {
            // Define base positions for the pentatonic scale
            return new Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)>
            {
                [ScaleBoxPosition.Box1] = (0, 3),   
                [ScaleBoxPosition.Box2] = (2, 5),   
                [ScaleBoxPosition.Box3] = (4, 8),   
                [ScaleBoxPosition.Box4] = (7, 10),   
                [ScaleBoxPosition.Box5] = (9, 12)   
            };
        }
    }
}
