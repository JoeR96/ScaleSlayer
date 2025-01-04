using CSharpFunctionalExtensions;
using ScaleSlayer.Application.Contracts.Persistence;
using ScaleSlayer.Application.Scales.Responses;

namespace ScaleSlayer.Application.Notes.Queries;

public class GetAllNotesQueryHandler(INoteRepository noteRepository)
    : IRequestHandler<GetAllNotesQuery, Result<GetAllNotesResponse>>
{
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));

    public async Task<Result<GetAllNotesResponse>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var fretNotes = await _noteRepository.GetAllNotesAsync(cancellationToken);

        var fretNoteDtos = fretNotes.Select(note => new FretNoteDto(note.Note, new NotePositionDto(note.NotePosition.StringNumber, note.NotePosition.FretNumber)));

        var response = new GetAllNotesResponse(fretNoteDtos);
        
        return Result.Success(response);
    }
}