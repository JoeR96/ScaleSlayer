using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Application.Contracts.Services;

public interface ILoggedInUserService
{
    UserId? UserId { get; }
}