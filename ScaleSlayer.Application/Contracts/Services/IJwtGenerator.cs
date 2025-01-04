using ScaleSlayer.Domain.UserAggregate;

namespace ScaleSlayer.Application.Contracts.Services;

public interface IJwtGenerator
{
    string CreateToken(User user);
}