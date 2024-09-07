using System.Text;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Application.Contracts.Services;
using LoopLearner.Domain.UserAggregate;
using LoopLearner.Infrastructure.Authentication;
using LoopLearner.Infrastructure.Persistence;
using LoopLearner.Infrastructure.Persistence.Interceptors;
using LoopLearner.Infrastructure.Persistence.Repositories;
using LoopLearner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LoopLearner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddPersistence(configuration)
                .AddAuth(configuration);

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));

            services.AddSingleton<IJwtGenerator, JwtGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        //OnAuthenticationFailed = context =>
                        //{
                        //    context.NoResult();
                        //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        //    context.Response.ContentType = "application/json";

                        //    var internalServerProblem = new ProblemDetails() { Status = context.Response.StatusCode, Type = "InternalServerError", Detail = "Authentication could not be completed" };
                        //    return context.Response.WriteAsJsonAsync(internalServerProblem);
                        //},
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var unauthorizedProblem = new ProblemDetails() { Status = context.Response.StatusCode, Type = "Unauthorized" };

                            return context.Response.WriteAsJsonAsync(unauthorizedProblem);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";

                            var forbiddenProblem = new ProblemDetails() { Status = context.Response.StatusCode, Type = "Forbidden" };
                            return context.Response.WriteAsJsonAsync(forbiddenProblem);
                        }
                    };
                });

            services.AddAuthorization();
            return services;

        }
        public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<LoopLearnerDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DatabaseConnectionString");
                options.UseSqlite(connectionString, b => b.MigrationsAssembly("LoopLearner.Web.Server"));
            });

            services.AddScoped<PublishDomainEventInterceptor>();
            services.AddScoped<AuditableInterceptor>();
            services.AddScoped<IUserRespository, UserRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IInstrumentPartRepository, InstrumentPartRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            return services;
        }

    }
}
