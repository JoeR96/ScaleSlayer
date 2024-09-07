using LoopLearner.Domain.SongAggregate.Entities;

namespace LoopLearner.Application.Contracts.Persistence;

public interface IChordRepository
{
    Task<IEnumerable<Chord>> GetChordCollectionAsync(IEnumerable<Guid> chordIds, CancellationToken cancellationToken);
}