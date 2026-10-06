
using FoodHub.Domain.Entity.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodHub.Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder) 
        {
            builder.HasOne(b => b.Order)
                   .WithMany(s => s.OrderItems)
                    .HasForeignKey(b => b.OrderId);

            builder.HasOne(b => b.Product)
                   .WithMany()
                   .HasForeignKey(b => b.ProductId);
                   

            builder.Property(b => b.Price)
                   .IsRequired()
                   .HasPrecision(18, 2);



        }

    }
}
