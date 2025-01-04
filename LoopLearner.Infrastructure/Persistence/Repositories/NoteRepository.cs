using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence.Repositories;

public class NoteRepository(LoopLearnerDbContext context) : INoteRepository
{
    private readonly LoopLearnerDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<FretNote>> GetAllNotesAsync(CancellationToken cancellationToken)
    {
        return await _context.Notes.Include(n => n.NotePosition).ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<FretNote>> GetFretNotesByNamesAsync(IEnumerable<Note> noteNames, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .Where(n => noteNames.Contains(n.Note))
            .Include(n => n.NotePosition)
            .ToListAsync(cancellationToken);
    }
}