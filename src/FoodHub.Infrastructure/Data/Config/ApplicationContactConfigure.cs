using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodHub.Infrastructure.Data.Config
{
    public class ApplicationContactConfigure : IEntityTypeConfiguration<ApplicationContact>
    {

        public void Configure(EntityTypeBuilder<ApplicationContact> builder) 
        {

            builder.HasKey(x => x.Id);

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(m => m.Phone)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(m => m.IDNumber)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasOne(m => m.MerchantApplication)
                   .WithMany(m => m.ApplicationContact)
                   .HasForeignKey(m => m.MerchantApplicationId)
                   .OnDelete(DeleteBehavior.Cascade);
                 
        }

    }
}
