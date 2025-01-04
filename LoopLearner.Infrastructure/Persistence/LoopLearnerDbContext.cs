using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.Common.Interfaces;
using LoopLearner.Domain.UserAggregate;
using LoopLearner.Infrastructure.Persistence.Configuration;
using LoopLearner.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence;

public class LoopLearnerDbContext(
    DbContextOptions<LoopLearnerDbContext> options,
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
            .ApplyConfigurationsFromAssembly(typeof(LoopLearnerDbContext).Assembly);

        AuditableConfiguration.Configure(modelBuilder);

        DataSeed.Seed(modelBuilder, passwordHasher);
        base.OnModelCreating(modelBuilder);
    }
}