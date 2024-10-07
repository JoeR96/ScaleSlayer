using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<FretNote>
{
    public void Configure(EntityTypeBuilder<FretNote> builder)
    {
        // Configure primary key
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id)
            .HasConversion(id => id.Value, value => NoteId.Create(value));

        builder.HasOne(n => n.Position)
            .WithOne()
            .HasForeignKey<FretNote>("NotePositionId")
            .IsRequired(); 

        builder.Property(n => n.Note)
            .HasConversion(noteName => noteName.ToString(), 
                value => (Note)Enum.Parse(typeof(Note), value))
            .IsRequired();
    }
}