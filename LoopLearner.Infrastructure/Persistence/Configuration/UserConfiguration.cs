using LoopLearner.Domain.UserAggregate;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoopLearner.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => UserId.Create(value));
        }
    }
}
