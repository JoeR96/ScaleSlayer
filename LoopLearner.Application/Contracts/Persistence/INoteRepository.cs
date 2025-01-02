using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Contracts.Persistence;

public interface INoteRepository
{
    Task<IEnumerable<FretNote>> GetFretNotesByNamesAsync(IEnumerable<Note> noteNames, CancellationToken cancellationToken);
    Task<IEnumerable<FretNote>> GetAllNotesAsync(CancellationToken cancellationToken);
}