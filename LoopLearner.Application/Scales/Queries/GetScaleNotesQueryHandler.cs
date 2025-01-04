using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Application.Scales.Responses;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public class GetScaleNotesQueryHandler(INoteRepository noteRepository)
    : IRequestHandler<GetScaleNotesQuery, Result<ScaleNotesResponse>>
{
    public async Task<Result<ScaleNotesResponse>> Handle(GetScaleNotesQuery request, CancellationToken cancellationToken)
    {
        var scale = Scale.CreateNew(request.RootNote, request.ScaleType);
        var noteNames = scale.GetScaleNoteNames();
        var notes = await noteRepository.GetFretNotesByNamesAsync(noteNames, cancellationToken);
        var boxNotes = scale.GroupNotesIntoBoxes(notes.ToList());

        return Result.Success(MapToDto(boxNotes));
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
