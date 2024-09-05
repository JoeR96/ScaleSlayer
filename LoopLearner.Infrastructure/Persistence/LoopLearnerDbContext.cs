using LoopLearner.Application.Contracts.Services;
using LoopLearner.Domain.Common.Interfaces;
using LoopLearner.Domain.UserAggregate;
using LoopLearner.Infrastructure.Persistence.Configuration;
using LoopLearner.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence;

public class LoopLearnerDbContext : DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;
    private readonly AuditableInterceptor _auditableInterceptor;

    public DbSet<User> Users { get; set; } = null!;

    public LoopLearnerDbContext(DbContextOptions<LoopLearnerDbContext> options, IDateTimeProvider dateTimeProvider, IPasswordHasher<User> passwordHasher, PublishDomainEventInterceptor publishDomainEventInterceptor, AuditableInterceptor auditableInterceptor) : base(options)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _passwordHasher = passwordHasher;
        _publishDomainEventInterceptor = publishDomainEventInterceptor;
        _auditableInterceptor = auditableInterceptor;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventInterceptor, _auditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(LoopLearnerDbContext).Assembly);

        AuditableConfiguration.Configure(modelBuilder);

        DataSeed.Seed(modelBuilder, _passwordHasher, _dateTimeProvider);
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}