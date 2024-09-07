using System.Security.Claims;
using LoopLearner.Application.Contracts.Services;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Web.Server.Services;

public class LoggedInUserService(IHttpContextAccessor httpContextAccessor) : ILoggedInUserService
{
    public UserId? UserId
    {
        get
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return null;
            return UserId.Create(new Guid(userId));
        }
    }

}