using CSharpFunctionalExtensions;
using LoopLearner.Domain.Errors;

namespace LoopLearner.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string UserName, string Email, string Password) : IRequest<Result<AuthenticationResponse, Error>>;