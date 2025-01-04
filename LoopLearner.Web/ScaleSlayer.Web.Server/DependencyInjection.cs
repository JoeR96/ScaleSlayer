using ScaleSlayer.Application.Contracts.Services;
using ScaleSlayer.Web.Server.Mappings;
using ScaleSlayer.Web.Server.Services;

namespace ScaleSlayer.Web.Server;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        services.AddMappings();
        return services;
    }
}