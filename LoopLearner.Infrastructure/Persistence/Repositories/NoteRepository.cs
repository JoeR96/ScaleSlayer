using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.SongAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence.Repositories;

public class NoteRepository(LoopLearnerDbContext context) : INoteRepository
{
    private readonly LoopLearnerDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<Note>> GetAllNotesAsync(CancellationToken cancellationToken)
    {
        return await _context.Notes.Include(n => n.Position).ToListAsync(cancellationToken);
    }
}