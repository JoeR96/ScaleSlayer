using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScaleSlayer.Domain.Common.Entities;
using ScaleSlayer.Domain.Common.Interfaces;
using ScaleSlayer.Domain.UserAggregate;
using ScaleSlayer.Infrastructure.Persistence.Configuration;
using ScaleSlayer.Infrastructure.Persistence.Interceptors;

namespace ScaleSlayer.Infrastructure.Persistence;

public class ScaleSlayerDbContext(
    DbContextOptions<ScaleSlayerDbContext> options,
    IPasswordHasher<User> passwordHasher,
    PublishDomainEventInterceptor publishDomainEventInterceptor,
    AuditableInterceptor auditableInterceptor)
    : DbContext(options)
{
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<FretNote> Notes { get; set; } = null!;
    public DbSet<NotePosition> NotePositions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(publishDomainEventInterceptor, auditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(ScaleSlayerDbContext).Assembly);

        AuditableConfiguration.Configure(modelBuilder);

        DataSeed.Seed(modelBuilder, passwordHasher);
        base.OnModelCreating(modelBuilder);
    }
}