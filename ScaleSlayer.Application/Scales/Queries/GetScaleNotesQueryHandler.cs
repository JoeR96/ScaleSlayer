using CSharpFunctionalExtensions;
using ScaleSlayer.Application.Contracts.Persistence;
using ScaleSlayer.Application.Scales.Responses;
using ScaleSlayer.Domain.Common.Entities;
using ScaleSlayer.Domain.ScaleAggregate;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.Application.Scales.Queries;

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
