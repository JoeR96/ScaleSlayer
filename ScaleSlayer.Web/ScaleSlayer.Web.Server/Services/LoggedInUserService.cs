using System.Security.Claims;
using ScaleSlayer.Application.Contracts.Services;
using ScaleSlayer.Domain.UserAggregate.ValueObjects;

namespace ScaleSlayer.Web.Server.Services;

public class LoggedInUserService(IHttpContextAccessor httpContextAccessor) : ILoggedInUserService
{
    public UserId? UserId
    {
        get
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return null;
            return UserId.Create(new Guid(userId));
        }
    }

}