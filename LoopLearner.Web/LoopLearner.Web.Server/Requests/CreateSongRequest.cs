using LoopLearner.Web.Server.Dto;

namespace LoopLearner.Web.Server.Requests;

public class CreateSongRequest
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Genre { get; set; }
    public int BPM { get; set; }
    public IEnumerable<InstrumentPartDto> InstrumentTabs { get; set; }
}