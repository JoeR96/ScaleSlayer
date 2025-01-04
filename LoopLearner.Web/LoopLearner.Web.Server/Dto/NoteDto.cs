using LoopLearner.Domain.ScaleAggregate.ValueObjects;

namespace LoopLearner.Web.Server.Dto;

public record FretNoteDto (Note Note, NotePositionDto Position);
public record NotePositionDto (int StringNumber, int FretNumber);
public record ScaleNotesResponse(Dictionary<ScaleBoxPosition, List<FretNoteDto>> ScaleNotes);
