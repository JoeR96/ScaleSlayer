using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence.Repositories;


public class InstrumentPartRepository : IInstrumentPartRepository
{
    private readonly LoopLearnerDbContext _context;

    public InstrumentPartRepository(LoopLearnerDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
    }

    public void AddInstrumentPart(InstrumentPart instrumentPart)
    {
        _context.InstrumentParts.Add(instrumentPart);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }

    public async Task<IEnumerable<InstrumentPart>> GetInstrumentPartsById(IEnumerable<InstrumentPartId> providedInstrumentPartIds)
    {
        return await _context.InstrumentParts.Where(a => providedInstrumentPartIds.Contains(a.Id)).ToListAsync();
    }
}