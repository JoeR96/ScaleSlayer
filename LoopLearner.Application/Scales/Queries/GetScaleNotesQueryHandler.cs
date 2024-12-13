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
            { Note.E, 0 }, 
            { Note.F, 1 }, 
            { Note.FSharp, 2 }, 
            { Note.G, 3 }, 
            { Note.GSharp, 4 }, 
            { Note.A, 5 }, 
            { Note.ASharp, 6 },
            { Note.B, 7 }, 
            { Note.C, 8 },
            { Note.CSharp, 9 }, 
            { Note.D, 10 }, 
            { Note.DSharp, 11 }
        };

        public GetScaleNotesQueryHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Result<Dictionary<ScaleBoxPosition, List<FretNote>>>> Handle(GetScaleNotesQuery request, CancellationToken cancellationToken)
        {
            var noteNames = GetScaleNoteNames(request.RootNote, request.ScaleType);
            var notes = await _noteRepository.GetFretNotesByNamesAsync(noteNames, cancellationToken);
            var boxNotes = GroupNotesIntoBoxes(notes.ToList(), request.RootNote, request.ScaleType);
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

        private Dictionary<ScaleBoxPosition, List<FretNote>> GroupNotesIntoBoxes(List<FretNote> notes, Note rootNote,
            ScaleType requestScaleType)
        {
            var boxFretPositions = ResolveBoxFretPositions(requestScaleType);
            var boxNotes = new Dictionary<ScaleBoxPosition, List<FretNote>>();

            //start at 0
            //lets workthrough in c#
            int rootOffset = noteOffsets[rootNote];
            //c# is 9
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

        private int WrapFretNumber(int fretNumber)
        {
            if (fretNumber < 0)
                return fretNumber + 23;
            if (fretNumber > 22)
                return fretNumber - 23;
            return fretNumber;
        }

        private Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)> ResolveBoxFretPositions(ScaleType scaleType)
        {
            if (scaleType == ScaleType.PentatonicMinor)
            {
                return new Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)>
                {
                    [ScaleBoxPosition.Box1] = (0, 3),   
                    [ScaleBoxPosition.Box2] = (2, 5),   
                    [ScaleBoxPosition.Box3] = (4, 8),   
                    [ScaleBoxPosition.Box4] = (7, 10),   
                    [ScaleBoxPosition.Box5] = (9, 12)   
                };
            }
            else // Major/Minor
            {
                return new Dictionary<ScaleBoxPosition, (int MinFret, int MaxFret)>
                {
                    [ScaleBoxPosition.Box1] = (1, 5),   
                    [ScaleBoxPosition.Box2] = (4, 8),   
                    [ScaleBoxPosition.Box3] = (6, 10),   
                    [ScaleBoxPosition.Box4] = (9, 13),   
                    [ScaleBoxPosition.Box5] = (11, 15)  
                };
            }
        }

    }
}
