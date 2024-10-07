using CSharpFunctionalExtensions;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public record GetScaleNotesAndChordsQuery(Note RootNote, ScaleType ScaleType) 
    : IRequest<Result<Scale, Error>>;