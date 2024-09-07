using LoopLearner.Domain.Common;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Domain.SongAggregate.Entities
{
    public class InstrumentPart : Entity<InstrumentPartId>
    {
        private readonly List<Tab> _tabs = new List<Tab>();
        public string InstrumentName { get; private set; }
        public IReadOnlyList<Tab> Tabs => _tabs.ToList();

        private InstrumentPart() { } 

        private InstrumentPart(InstrumentPartId id, string instrumentName, IEnumerable<Tab> tabs)
        {
            Id = id;
            InstrumentName = instrumentName;
            _tabs = tabs.ToList();
        }

        public static InstrumentPart Create(InstrumentPartId id, string instrumentName, IEnumerable<Tab> tabs) =>
            new(id, instrumentName, tabs);

        public static InstrumentPart CreateNew(string instrumentName, IEnumerable<Tab> tabs) =>
            new(InstrumentPartId.CreateNew(), instrumentName, tabs);

        public void AddTab(Tab tab)
        {
            _tabs.Add(tab);
        }
    }
}