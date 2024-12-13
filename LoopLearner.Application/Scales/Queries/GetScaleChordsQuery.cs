using CSharpFunctionalExtensions;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public record GetScaleChordsQuery(Note RootNote, ScaleType ScaleType) : IRequest<Result<List<ChordDto>>>;
