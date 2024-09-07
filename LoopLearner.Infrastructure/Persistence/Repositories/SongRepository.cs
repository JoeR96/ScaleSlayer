using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.SongAggregate;

namespace LoopLearner.Infrastructure.Persistence.Repositories;

public class SongRepository : ISongRepository
{
    private readonly LoopLearnerDbContext _context;

    public SongRepository(LoopLearnerDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
    }
    public void AddSong(Song book)
    {
        _context.Songs.Add(book);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}