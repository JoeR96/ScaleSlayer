using LoopLearner.Application.Contracts.Services;
using LoopLearner.Web.Server.Mappings;
using LoopLearner.Web.Server.Services;

namespace LoopLearner.Web.Server;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        services.AddMappings();
        return services;
    }
}