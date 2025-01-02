using CSharpFunctionalExtensions;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.Errors;

namespace LoopLearner.Application.Notes.Queries;

public record GetAllNotesQuery() : IRequest<Result<IEnumerable<FretNote>, Error>>;
