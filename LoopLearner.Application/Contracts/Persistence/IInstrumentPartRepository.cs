using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Application.Contracts.Persistence;

public interface IInstrumentPartRepository
{
    void AddInstrumentPart(InstrumentPart book);
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<InstrumentPart>> GetInstrumentPartsById(IEnumerable<InstrumentPartId> providedInstrumentPartIds);
}