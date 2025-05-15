using api_bulk_fill_v2.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_bulk_fill_v2.Infrastructure.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Map to table "Users"
            builder.ToTable("Users");

            // Primary key
            builder.HasKey(u => u.UserId);

            // Auto-increment identity column
            builder.Property(u => u.UserId)
                   .ValueGeneratedOnAdd();

            // Name: required, max length 200
            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            // Email: required, max length 255, unique
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.HasIndex(u => u.Email)
                   .IsUnique();
        }
    }
}
