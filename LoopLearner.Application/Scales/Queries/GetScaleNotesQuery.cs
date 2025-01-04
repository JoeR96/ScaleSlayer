using CSharpFunctionalExtensions;
using LoopLearner.Application.Scales.Responses;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public record GetScaleNotesQuery(Note RootNote, ScaleType ScaleType) : IRequest<Result<ScaleNotesResponse>>;
