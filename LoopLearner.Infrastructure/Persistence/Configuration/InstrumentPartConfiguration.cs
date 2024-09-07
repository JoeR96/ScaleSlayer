using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LoopLearner.Infrastructure.Persistence.Configuration;

public class InstrumentPartConfiguration : IEntityTypeConfiguration<InstrumentPart>
{
    public void Configure(EntityTypeBuilder<InstrumentPart> builder)
    {
        builder.HasKey(ip => ip.Id);
        builder.Property(ip => ip.Id)
            .HasConversion(id => id.Value, value => InstrumentPartId.Create(value));

        builder.Property(ip => ip.InstrumentName)
            .IsRequired()
            .HasMaxLength(100);  

        builder.HasMany(ip => ip.Tabs)
            .WithOne()  
            .HasForeignKey("InstrumentPartId") 
            .IsRequired();

        builder.Navigation(ip => ip.Tabs)
            .HasField("_tabs")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
