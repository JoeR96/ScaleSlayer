using CSharpFunctionalExtensions;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.SongAggregate.Entities;

namespace LoopLearner.Application.Songs.Queries;

public record GetAllNotesQuery() : IRequest<Result<IEnumerable<Note>, Error>>;
