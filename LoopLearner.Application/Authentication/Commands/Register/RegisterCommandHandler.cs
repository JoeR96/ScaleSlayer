using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Application.Contracts.Services;
using LoopLearner.Application.Extensions;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.Errors.Authentication;
using LoopLearner.Domain.Errors.General;
using LoopLearner.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace LoopLearner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResponse, Error>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRespository _userRespository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterCommandHandler(IJwtGenerator jwtGenerator, IUserRespository userRespository, IPasswordHasher<User> passwordHasher)
    {
        _jwtGenerator = jwtGenerator;
        _userRespository = userRespository;
        _passwordHasher = passwordHasher;
    }
    public async Task<Result<AuthenticationResponse, Error>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        Result<bool, ValidationError> validationResult = await request.ValidateAsync(new RegisterCommandValidator(), cancellationToken);
        if (validationResult.IsFailure)
            return validationResult.Error;


        if (await _userRespository.GetUserByEmailAsync(request.Email) != null)
        {
            return new EmailAlreadyInUseError();
        }
        if (await _userRespository.GetUserByUserNameAsync(request.UserName) != null)
        {
            return new UserNameAlreadyTakenError();
        }

        User user = User.CreateNew(request.FirstName, request.LastName, request.UserName, request.Email, request.Password);
        string hashedPassword = _passwordHasher.HashPassword(user, request.Password);
        user.UpdatePassword(hashedPassword);

        _userRespository.AddUser(user);
        await _userRespository.SaveChangesAsync();

        string token = _jwtGenerator.CreateToken(user);
        return new AuthenticationResponse(user, token);
    }
}