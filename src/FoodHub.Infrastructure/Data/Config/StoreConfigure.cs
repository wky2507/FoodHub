using FoodHub.Domain.Entity.StoreEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FoodHub.Infrastructure.Data.Config
{
    public class StoreConfigure : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder) {

            builder.HasKey(x => x.Id);

            builder.Property(b => b.StoreName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.StorePhone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(b => b.StorePhone)
                   .IsUnique();


            builder.Property(b => b.StoreAddress)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasOne(m => m.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            var nav = builder.Metadata.FindNavigation(nameof(Store.ApplicationUser));

            nav?.SetPropertyAccessMode(PropertyAccessMode.Field);

        }




    }
}
