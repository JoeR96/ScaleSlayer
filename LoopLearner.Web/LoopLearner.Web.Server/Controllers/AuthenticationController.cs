using LoopLearner.Application.Authentication.Commands.Register;
using LoopLearner.Application.Authentication.Queries;
using LoopLearner.Web.Server.Models.Authentication;

namespace LoopLearner.Web.Server.Controllers;

using AutoMapper;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[AllowAnonymous]
public class AuthenticationController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var result = await _mediator.Send(query);
        if (result.IsFailure)
            return Problem(result.Error);

        AuthResponse authResponse = _mapper.Map<AuthResponse>(result.Value);
        return Ok(authResponse);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.UserName, request.Email, request.Password);
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Problem(result.Error);

        AuthResponse authResponse = _mapper.Map<AuthResponse>(result.Value);
        return Ok(authResponse);
    }
}

