using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration;

public class ChordConfiguration : IEntityTypeConfiguration<Chord>
{
    public void Configure(EntityTypeBuilder<Chord> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(id => id.Value, value => ChordId.Create(value));

        builder.Property(e => e.RootNote)
            .HasConversion(rootNote => rootNote.ToString(), 
                value => (NoteName)Enum.Parse(typeof(NoteName), value))
            .IsRequired();

        builder.Property(e => e.Type)
            .HasConversion(type => type.ToString(), 
                value => (ChordType)Enum.Parse(typeof(ChordType), value))
            .IsRequired();

        builder.Property(e => e.Extension)
            .HasConversion(extension => extension.ToString(), 
                value => (ChordExtension)Enum.Parse(typeof(ChordExtension), value));

        // Configure the relationship between Chord and Notes
        builder.HasMany(e => e.Notes)
            .WithMany()  // Assuming no navigation property back from Note to Chord
            .UsingEntity<Dictionary<string, object>>(
                "ChordNote", // Join table name
                j => j.HasOne<Note>()
                    .WithMany()
                    .HasForeignKey("NoteId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Chord>()
                    .WithMany()
                    .HasForeignKey("ChordId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}