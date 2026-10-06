using FoodHub.Domain.Entity.BuyerAggregate;
using FoodHub.Domain.Entity.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodHub.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder) {

            var nav = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

            nav?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(b => b.TotalPrice)
                   .HasPrecision(18, 2);

            builder.Property(b => b.Address)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(b => b.Store)
                   .WithMany()
                   .HasForeignKey(b => b.StoreId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Buyer)
                   .WithMany()
                   .HasForeignKey(b => b.BuyerId)
                   .OnDelete(DeleteBehavior.NoAction);
           
        
        }


    }
}
