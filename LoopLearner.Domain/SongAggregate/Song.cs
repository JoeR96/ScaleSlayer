using LoopLearner.Domain.Common;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.Events;
using LoopLearner.Domain.UserAggregate.ValueObjects;


namespace LoopLearner.Domain.SongAggregate
{
    public class Song : AuditableAggregate<SongId>
    {
        private readonly List<InstrumentPart> _instrumentParts = new List<InstrumentPart>();
        public SongId Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Genre { get; private set; }
        public int BPM { get; private set; }
        public IReadOnlyList<InstrumentPart> InstrumentParts => _instrumentParts.ToList();

        private Song() { } // For EF Core
        private Song(SongId id, string title, string artist, string genre, int bpm)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Genre = genre;
            BPM = bpm;
        }

        public static Song Create(SongId id, string title, string artist, string genre, int bpm) => new Song(id, title, artist, genre, bpm);

        public static Song CreateNew(string title, string artist, string genre, int bpm)
        {
            Song song = new Song(SongId.CreateNew(), title, artist, genre, bpm);
            song.AddDomainEvent(new SongCreatedEvent(song));
            return song;
        }

        public void AddInstrumentPart(InstrumentPart instrumentPart)
        {
            _instrumentParts.Add(instrumentPart);
        }
    }
}