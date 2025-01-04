using ScaleSlayer.Domain.UserAggregate.ValueObjects;

namespace ScaleSlayer.Application.Contracts.Services;

public interface ILoggedInUserService
{
    UserId? UserId { get; }
}