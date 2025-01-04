using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Application.Scales.Responses;

namespace LoopLearner.Application.Notes.Queries;

public class GetAllNotesQueryHandler(INoteRepository noteRepository)
    : IRequestHandler<GetAllNotesQuery, Result<GetAllNotesResponse>>
{
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));

    public async Task<Result<GetAllNotesResponse>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var fretNotes = await _noteRepository.GetAllNotesAsync(cancellationToken);

        var fretNoteDtos = fretNotes.Select(note => new FretNoteDto(note.Note, new NotePositionDto(note.Position.StringNumber, note.Position.FretNumber)));

        var response = new GetAllNotesResponse(fretNoteDtos);
        
        return Result.Success(response);
    }
}