using Microsoft.EntityFrameworkCore;
using FoodHub.Domain.Entity.StoreEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FoodHub.Infrastructure.Data.Config
{
    public class StoreContactConfigure :IEntityTypeConfiguration<StoreContact>
    {
        public void Configure(EntityTypeBuilder<StoreContact> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(m => m.Phone)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(m => m.IDNumber)
                   .IsRequired();

            builder.Property(m => m.Image_IDCard_Url)
                   .IsRequired();

            builder.HasOne(m => m.Store)
                   .WithMany(m => m.StoreContacts)
                   .HasForeignKey(m => m.StoreId);

        }

    }
}
