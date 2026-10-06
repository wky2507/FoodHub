using FoodHub.Domain.Entity.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodHub.Infrastructure.Data.Config
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder) {

            builder.HasOne(b => b.Basket)
                   .WithMany(s => s.Items)
                   .HasForeignKey(b => b.BasketId);

            builder.Property(b => b.UnitPrice)
                   .HasPrecision(18, 2);
        
        }

    }
}
