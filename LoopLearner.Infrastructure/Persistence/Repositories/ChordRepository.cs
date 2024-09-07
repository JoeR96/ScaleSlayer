using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.SongAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence.Repositories;

public class ChordRepository(LoopLearnerDbContext context) : IChordRepository
{
    public async Task<IEnumerable<Chord>> GetChordCollectionAsync(IEnumerable<Guid> chordIds, CancellationToken cancellationToken)
    {
        return await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .Where(c => chordIds.Contains(c.Id.Value))
            .ToListAsync(cancellationToken);
    }
}