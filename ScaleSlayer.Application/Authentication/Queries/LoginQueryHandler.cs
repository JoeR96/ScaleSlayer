using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using ScaleSlayer.Application.Contracts.Persistence;
using ScaleSlayer.Application.Contracts.Services;
using ScaleSlayer.Application.Extensions;
using ScaleSlayer.Domain.Errors;
using ScaleSlayer.Domain.Errors.Authentication;
using ScaleSlayer.Domain.Errors.General;
using ScaleSlayer.Domain.UserAggregate;

namespace ScaleSlayer.Application.Authentication.Queries;

public class LoginQueryHandler(
    IJwtGenerator jwtGenerator,
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher)
    : IRequestHandler<LoginQuery, Result<AuthenticationResponse, Error>>
{
    public async Task<Result<AuthenticationResponse, Error>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        
        Result<bool, ValidationError> validationResult = await request.ValidateAsync(new LoginQueryValidator(), cancellationToken);
        
        if (validationResult.IsFailure)
            return validationResult.Error;

        User? user = await userRepository.GetUserByEmailAsync(request.Email);
        if (user is null)
            return new InvalidCredentialsError("Invalid email or password");

        PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return new InvalidCredentialsError("Invalid email or password");

        string token = jwtGenerator.CreateToken(user);

        return new AuthenticationResponse(user, token);

    }
}