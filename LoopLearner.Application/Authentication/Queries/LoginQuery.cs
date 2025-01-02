using CSharpFunctionalExtensions;
using LoopLearner.Domain.Errors;

namespace LoopLearner.Application.Authentication.Queries;

public record LoginQuery(string Email, string Password) : IRequest<Result<AuthenticationResponse, Error>> { }