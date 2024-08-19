using Exams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exams.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.PhoneNumber).IsUnique();
            builder.Property(user => user.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(user => user.LastName).IsRequired().HasMaxLength(128);
            builder.Property(user => user.PhoneNumber).IsRequired().HasMaxLength(128);
        }
    }
}
