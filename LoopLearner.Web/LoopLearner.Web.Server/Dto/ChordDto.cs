using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Web.Server.Dto;

public class ChordDto
{
    public int Order { get; set; }
    public NoteName RootNote { get; set; }
    public ChordType ChordType { get; set; }
    public ChordExtension ChordExtension { get; set; }
    public StringNumber RootString { get; set; }
    public FretNumber RootFret { get; set; }
    public List<NoteDto> Notes { get; set; }
}