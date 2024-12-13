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

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResponse, Error>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRespository _userRespository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public LoginQueryHandler(IJwtGenerator jwtGenerator, IUserRespository userRespository, IPasswordHasher<User> passwordHasher)
    {
        _jwtGenerator = jwtGenerator;
        _userRespository = userRespository;
        _passwordHasher = passwordHasher;
    }
    public async Task<Result<AuthenticationResponse, Error>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        
        Result<bool, ValidationError> validationResult = await request.ValidateAsync(new LoginQueryValidator(), cancellationToken);
        if (validationResult.IsFailure)
            return validationResult.Error;

        User? user = await _userRespository.GetUserByEmailAsync(request.Email);
        if (user is null)
            return new InvalidCredentialsError("Invalid email or password");

        PasswordVerificationResult passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return new InvalidCredentialsError("Invalid email or password");

        string token = _jwtGenerator.CreateToken(user);

        return new AuthenticationResponse(user, token);

    }
}