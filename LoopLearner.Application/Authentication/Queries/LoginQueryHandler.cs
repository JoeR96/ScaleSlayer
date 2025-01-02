using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Application.Contracts.Services;
using LoopLearner.Application.Extensions;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.Errors.Authentication;
using LoopLearner.Domain.Errors.General;
using LoopLearner.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace LoopLearner.Application.Authentication.Queries;

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