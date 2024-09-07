using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.Errors.General;
using LoopLearner.Domain.SongAggregate.Entities;

namespace LoopLearner.Application.Songs.Queries;

public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, Result<IEnumerable<Note>, Error>>
{
    private readonly INoteRepository _noteRepository;

    public GetAllNotesQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    }

    public async Task<Result<IEnumerable<Note>, Error>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetAllNotesAsync(cancellationToken);

        if (!notes.Any())
            return new NotFoundError("No notes were found.");

        return notes.ToList();
    }
}