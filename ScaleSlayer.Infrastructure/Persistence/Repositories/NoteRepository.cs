using Microsoft.EntityFrameworkCore;
using ScaleSlayer.Application.Contracts.Persistence;
using ScaleSlayer.Domain.Common.Entities;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.Infrastructure.Persistence.Repositories;

public class NoteRepository(ScaleSlayerDbContext context) : INoteRepository
{
    private readonly ScaleSlayerDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

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