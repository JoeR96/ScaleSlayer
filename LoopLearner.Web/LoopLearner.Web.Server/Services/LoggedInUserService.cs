using System.Security.Claims;
using LoopLearner.Application.Contracts.Services;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Web.Server.Services;

public class LoggedInUserService : ILoggedInUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserId? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return null;
            return UserId.Create(new Guid(userId));
        }
    }

}