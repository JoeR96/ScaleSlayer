using CSharpFunctionalExtensions;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public record GetScaleNotesQuery(Note RootNote, ScaleType ScaleType) : IRequest<Result<Dictionary<ScaleBoxPosition, List<FretNote>>>>;
