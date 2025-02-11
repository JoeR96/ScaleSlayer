﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ScaleSlayer.Application.Contracts.Persistence;
using ScaleSlayer.Application.Contracts.Services;
using ScaleSlayer.Domain.UserAggregate;
using ScaleSlayer.Infrastructure.Authentication;
using ScaleSlayer.Infrastructure.Persistence;
using ScaleSlayer.Infrastructure.Persistence.Interceptors;
using ScaleSlayer.Infrastructure.Persistence.Repositories;
using ScaleSlayer.Infrastructure.Services;

namespace ScaleSlayer.Infrastructure
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
            services.AddDbContext<ScaleSlayerDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DatabaseConnectionString");
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ScaleSlayer.Web.Server"));
            });

            services.AddScoped<PublishDomainEventInterceptor>();
            services.AddScoped<AuditableInterceptor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            return services;
        }

    }
}
