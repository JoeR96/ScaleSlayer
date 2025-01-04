using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration;

public class NoteConfiguration : IEntityTypeConfiguration<FretNote>
{
    public void Configure(EntityTypeBuilder<FretNote> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id)
            .HasConversion(id => id.Value, value => NoteId.Create(value));

        builder.HasOne(n => n.NotePosition)
            .WithOne()
            .HasForeignKey<FretNote>("NotePositionId")
            .IsRequired(); 

        builder.Property(n => n.Note)
            .HasConversion(noteName => noteName.ToString(), 
                value => (Note)Enum.Parse(typeof(Note), value))
            .IsRequired();
    }
}