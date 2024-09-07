using LoopLearner.Domain.SongAggregate;

namespace LoopLearner.Application.Contracts.Persistence;

public interface ISongRepository
{
    void AddSong(Song book);
    Task<bool> SaveChangesAsync();
}