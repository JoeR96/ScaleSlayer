using LoopLearner.Application.Contracts.Services;
using LoopLearner.Domain.Common.Interfaces;
using LoopLearner.Domain.SongAggregate;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate;
using LoopLearner.Infrastructure.Persistence.Configuration;
using LoopLearner.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence;

public class LoopLearnerDbContext(
    DbContextOptions<LoopLearnerDbContext> options,
    IDateTimeProvider dateTimeProvider,
    IPasswordHasher<User> passwordHasher,
    PublishDomainEventInterceptor publishDomainEventInterceptor,
    AuditableInterceptor auditableInterceptor)
    : DbContext(options)
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<Song> Songs { get; set; } = null!;
    public DbSet<FretNote> Notes { get; set; } = null!;
    public DbSet<NotePosition> NotePositions { get; set; } = null!;
    public DbSet<InstrumentPart> InstrumentParts { get; set; } = null!;
    public DbSet<Chord> Chords { get; set; }

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
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}