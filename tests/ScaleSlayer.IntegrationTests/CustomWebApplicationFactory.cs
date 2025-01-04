using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScaleSlayer.Infrastructure.Persistence;
using ScaleSlayer.IntegrationTests.Extensions;
using Testcontainers.PostgreSql;

namespace ScaleSlayer.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private PostgreSqlContainer _postgresContainer = null!;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.ReplaceDbContextWithPostgresTestContainer<ScaleSlayerDbContext>(_postgresContainer.GetConnectionString(), "ScaleSlayer.Web.Server");
        });
    }

    public async Task StarContainerAsync()
    {
        _postgresContainer = new PostgreSqlBuilder()
            .WithDatabase("scale-slayer")
            .WithUsername("postgres")
            .WithPassword("password")
            .WithCleanUp(true)
            .Build();
        
        await _postgresContainer.StartAsync();
        
        using  var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ScaleSlayerDbContext>();
        await dbContext.Database.MigrateAsync();
        await DataSeed.SeedNotes(dbContext);
    }
    
    public new async Task DisposeAsync() => await _postgresContainer.DisposeAsync();
}