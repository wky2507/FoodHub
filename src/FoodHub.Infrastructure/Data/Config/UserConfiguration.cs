using Microsoft.EntityFrameworkCore;
using FoodHub.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) {

            builder.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            
            builder.Property(b => b.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.HasIndex(b => b.Phone)
                   .IsUnique();

            builder.Property(b => b.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(256);
        }

    }
}
