using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Contracts.Persistence;

public interface INoteRepository
{
    Task<IEnumerable<FretNote>> GetAllNotesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<FretNote>> GetFretNotesByNamesAsync(IEnumerable<Note> noteNames, CancellationToken cancellationToken);
}