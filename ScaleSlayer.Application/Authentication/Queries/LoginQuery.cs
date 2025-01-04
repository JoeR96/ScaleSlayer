using CSharpFunctionalExtensions;
using ScaleSlayer.Domain.Errors;

namespace ScaleSlayer.Application.Authentication.Queries;

public record LoginQuery(string Email, string Password) : IRequest<Result<AuthenticationResponse, Error>> { }