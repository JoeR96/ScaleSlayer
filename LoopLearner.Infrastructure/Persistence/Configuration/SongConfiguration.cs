using LoopLearner.Domain.SongAggregate;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration;

public class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(id => id.Value, value => SongId.Create(value));

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200); // Adjust the length if necessary

        builder.Property(s => s.Artist)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Genre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.BPM)
            .IsRequired();

        builder.HasMany(s => s.InstrumentParts)
            .WithOne()
            .HasForeignKey("SongId") // Assuming InstrumentPart has a foreign key property named SongId
            .IsRequired();

        builder.Navigation(s => s.InstrumentParts)
            .HasField("_instrumentParts")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}