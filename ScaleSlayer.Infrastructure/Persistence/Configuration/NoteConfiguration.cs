using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScaleSlayer.Domain.Common.Entities;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.Infrastructure.Persistence.Configuration;

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