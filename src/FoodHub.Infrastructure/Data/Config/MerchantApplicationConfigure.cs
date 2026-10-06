using Microsoft.EntityFrameworkCore;

using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FoodHub.Infrastructure.Data.Config
{
    public class MerchantApplicationConfigure:IEntityTypeConfiguration<MerchantApplication>
    {

        public void Configure(EntityTypeBuilder<MerchantApplication> builder) 
        {

            builder.HasKey(m => m.Id);
            
            //builder.HasIndex(m => m.UserId)
            //       .IsUnique();
                   
            builder.HasOne(m => m.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(m => m.UserId);
         
            builder.Property(m => m.StoreName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(m => m.Address)
                    .IsRequired()
                    .HasMaxLength(255);

            builder.Property(m => m.Phone)
                   .IsRequired();
                  
            builder.Property(m => m.LicenseImage_Url)
                   .IsRequired();

            builder.Property(m => m.SubmittedAt)
                   .IsRequired();
            
            var nav = builder.Metadata.FindNavigation(nameof(MerchantApplication.ApplicationContact));

            nav?.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        }

    }
}
