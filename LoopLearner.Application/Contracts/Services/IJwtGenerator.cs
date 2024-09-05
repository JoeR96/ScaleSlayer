using LoopLearner.Domain.UserAggregate;

namespace LoopLearner.Application.Contracts.Services;

public interface IJwtGenerator
{
    string CreateToken(User user);
}