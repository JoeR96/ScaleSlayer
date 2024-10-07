using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Contracts.Persistence;

public interface IChordRepository
{
    Task<IEnumerable<Chord>> GetChordCollectionAsync(IEnumerable<Guid> chordIds, CancellationToken cancellationToken);
    Task<List<Chord>> GetChordsForScaleAsync(Note cSharp, ScaleType minor, CancellationToken cancellationToken);
}