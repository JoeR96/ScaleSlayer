// using CSharpFunctionalExtensions;
// using LoopLearner.Application.Contracts.Persistence;
// using LoopLearner.Domain.Errors;
// using LoopLearner.Domain.Errors.General;
// using LoopLearner.Domain.ScaleAggregate;
// using LoopLearner.Domain.ScaleAggregate.ValueObjects;
// using LoopLearner.Domain.SongAggregate.Entities;
// using LoopLearner.Domain.SongAggregate.ValueObjects;
// using MediatR;
//
// namespace LoopLearner.Application.Scales.Queries;
//
// public class GetScaleNotesAndChordsQueryHandler : IRequestHandler<GetScaleNotesAndChordsQuery, Result<Scale, Error>>
// {
//     private readonly INoteRepository _noteRepository;
//
//     public GetScaleNotesAndChordsQueryHandler(INoteRepository noteRepository)
//     {
//         _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
//     }
//
//     public async Task<Result<Scale, Error>> Handle(GetScaleNotesAndChordsQuery request, CancellationToken cancellationToken)
//     {
//         // Get the scale notes based on the root note and scale type
//         var noteNames = GetScaleNoteNames(request.RootNote, request.ScaleType);
//
//         // Retrieve the notes from the repository
//         var notes = await _noteRepository.GetNotesByNamesAsync(noteNames, cancellationToken);
//
//         if (!notes.Any())
//         {
//             return Result.Failure<Scale, Error>(new NotFoundError("Notes for the requested scale were not found."));
//         }
//
//         // Group notes into box positions
//         var boxNotes = GroupNotesByBoxPosition(notes);
//
//         // Create a Scale object and set the scale notes
//         var scale = Scale.CreateNew(request.RootNote, request.ScaleType);
//         // scale.SeedNotes(boxNotes);
//
//         return Result.Success(scale);
//     }
//
//     private List<NoteName> GetScaleNoteNames(NoteName rootNote, ScaleType scaleType)
//     {
//         // For C# minor pentatonic scale
//         if (rootNote == NoteName.CSharp && scaleType == ScaleType.PentatonicMinor)
//         {
//             return new List<NoteName> { NoteName.CSharp, NoteName.E, NoteName.FSharp, NoteName.GSharp, NoteName.B };
//         }
//
//         return new List<NoteName>();
//     }
//
//     private Dictionary<ScaleBoxPosition, List<Note>> GroupNotesByBoxPosition(IEnumerable<Note> notes)
//     {
//         return new Dictionary<ScaleBoxPosition, List<Note>>
//         {
//             [ScaleBoxPosition.Box1] = notes.Where(n => n.Position.FretNumber >= 9 && n.Position.FretNumber <= 12).ToList(),
//             [ScaleBoxPosition.Box2] = notes.Where(n => n.Position.FretNumber >= 11 && n.Position.FretNumber <= 14).ToList(),
//             [ScaleBoxPosition.Box3] = notes.Where(n => n.Position.FretNumber >= 14 && n.Position.FretNumber <= 17).ToList(),
//             [ScaleBoxPosition.Box4] = notes.Where(n => n.Position.FretNumber >= 16 && n.Position.FretNumber <= 19).ToList(),
//             [ScaleBoxPosition.Box5] = notes.Where(n => n.Position.FretNumber >= 19 && n.Position.FretNumber <= 21).ToList()
//         };
//     }
// }