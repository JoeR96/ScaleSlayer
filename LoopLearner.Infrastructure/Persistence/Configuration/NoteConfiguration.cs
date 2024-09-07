using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        // Configure primary key
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id)
            .HasConversion(id => id.Value, value => NoteId.Create(value));

        builder.HasOne(n => n.Position)
            .WithOne()
            .HasForeignKey<Note>("NotePositionId")
            .IsRequired(); 

        builder.Property(n => n.NoteName)
            .HasConversion(noteName => noteName.ToString(), 
                value => (NoteName)Enum.Parse(typeof(NoteName), value))
            .IsRequired();
    }
}