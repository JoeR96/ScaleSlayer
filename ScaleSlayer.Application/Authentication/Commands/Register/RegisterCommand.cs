using CSharpFunctionalExtensions;
using ScaleSlayer.Domain.Errors;

namespace ScaleSlayer.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string UserName, string Email, string Password) : IRequest<Result<AuthenticationResponse, Error>>;