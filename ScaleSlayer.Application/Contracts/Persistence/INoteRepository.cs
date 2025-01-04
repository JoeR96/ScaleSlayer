using ScaleSlayer.Domain.Common.Entities;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.Application.Contracts.Persistence;

public interface INoteRepository
{
    Task<IEnumerable<FretNote>> GetFretNotesByNamesAsync(IEnumerable<Note> noteNames, CancellationToken cancellationToken);
    Task<IEnumerable<FretNote>> GetAllNotesAsync(CancellationToken cancellationToken);
}