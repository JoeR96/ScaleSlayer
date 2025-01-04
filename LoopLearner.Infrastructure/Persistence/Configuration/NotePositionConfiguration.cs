using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration;

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