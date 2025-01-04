using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Web.Server.Dto;

public record FretNoteDto (Note note, NotePositionDto position);
public record NotePositionDto (int stringNumber, int fretNumber);
public record ScaleNotesResponse(Dictionary<ScaleBoxPosition, List<FretNoteDto>> ScaleNotes);
