using LoopLearner.Domain.SongAggregate.Entities;

namespace LoopLearner.Web.Server.Dto;

public class InstrumentPartDto
{
    public Guid? Id { get; set; }
    public string InstrumentName { get; set; } // No need for private set
    public IEnumerable<TabDto> Tabs { get; set; } // Changed to TabDto
}