using CSharpFunctionalExtensions;
using ScaleSlayer.Application.Scales.Responses;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.Application.Scales.Queries;

public record GetScaleNotesQuery(Note RootNote, ScaleType ScaleType) : IRequest<Result<ScaleNotesResponse>>;
