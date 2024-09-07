namespace LoopLearner.Web.Server.Dto;

public class SongDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Genre { get; set; }
    public int BPM { get; set; }
    public IEnumerable<InstrumentPartDto> InstrumentParts { get; set; }
}