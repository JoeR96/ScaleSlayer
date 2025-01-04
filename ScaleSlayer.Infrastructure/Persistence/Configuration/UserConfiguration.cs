using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScaleSlayer.Domain.UserAggregate;
using ScaleSlayer.Domain.UserAggregate.ValueObjects;

namespace ScaleSlayer.Infrastructure.Persistence.Configuration
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
