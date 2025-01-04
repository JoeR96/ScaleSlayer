using CSharpFunctionalExtensions;
using LoopLearner.Application.Scales.Responses;

namespace LoopLearner.Application.Notes.Queries;

public record GetAllNotesQuery : IRequest<Result<GetAllNotesResponse>>;
