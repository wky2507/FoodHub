using Microsoft.EntityFrameworkCore;
using FoodHub.Domain.Entity.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FoodHub.Infrastructure.Data.Config
{
    public class AppUserRefreshTokenConfigure :IEntityTypeConfiguration<AppUserRefreshToken>
    {

        public void Configure(EntityTypeBuilder<AppUserRefreshToken> builder) {

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(c => c.UserId);

        }

    }
}
