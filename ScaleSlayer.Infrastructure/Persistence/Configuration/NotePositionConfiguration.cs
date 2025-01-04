using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScaleSlayer.Domain.Common.Entities;
using ScaleSlayer.Domain.SongAggregate.ValueObjects;

namespace ScaleSlayer.Infrastructure.Persistence.Configuration;

public class NotePositionConfiguration : IEntityTypeConfiguration<NotePosition>
{
    public void Configure(EntityTypeBuilder<NotePosition> builder)
    {
        builder.HasKey(np => np.Id);
        builder.Property(np => np.Id)
            .HasConversion(id => id.Value, value => NotePositionId.Create(value));

        builder.Property(np => np.StringNumber)
            .IsRequired();
                
        builder.Property(np => np.FretNumber)
            .IsRequired();
    }
}