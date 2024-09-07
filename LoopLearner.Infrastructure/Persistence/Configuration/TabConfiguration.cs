using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration;

public class TabConfiguration : IEntityTypeConfiguration<Tab>
{
    public void Configure(EntityTypeBuilder<Tab> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(id => id.Value, value => TabId.Create(value));

        builder.HasMany(t => t.Chords)
            .WithOne()
            .HasForeignKey("TabId");

        builder.HasMany(t => t.Notes)
            .WithOne()
            .HasForeignKey("TabId");

        builder.Navigation(t => t.Chords)
            .HasField("_chords")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(t => t.Notes)
            .HasField("_notes")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}