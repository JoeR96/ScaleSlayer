using LoopLearner.Domain.SongAggregate.Entities;

namespace LoopLearner.Application.Contracts.Persistence;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllNotesAsync(CancellationToken cancellationToken);
}