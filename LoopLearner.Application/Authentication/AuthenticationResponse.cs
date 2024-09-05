using LoopLearner.Domain.UserAggregate;

namespace LoopLearner.Application.Authentication;

public record AuthenticationResponse(User User, string Token);
