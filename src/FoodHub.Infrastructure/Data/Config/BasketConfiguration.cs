using FoodHub.Domain.Entity.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FoodHub.Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder){

            var nevigation = builder.Metadata.FindNavigation(nameof(Basket.Items));

            nevigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(b => b.BuyerId)
                   .IsRequired()
                   .HasMaxLength(100);

        }
    }
}
