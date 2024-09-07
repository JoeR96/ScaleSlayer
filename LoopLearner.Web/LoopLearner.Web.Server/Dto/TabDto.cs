namespace LoopLearner.Web.Server.Dto;

public class TabDto
{
    public IEnumerable<ChordDto> Chords { get; set; }
    public IEnumerable<NoteDto> Notes { get; set; }
}