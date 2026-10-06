using FoodHub.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FoodHub.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.Price)
                   .IsRequired()
                   .HasPrecision(18,2);

            builder.HasOne(b => b.Store)
                    .WithMany()
                    .HasForeignKey(b => b.StoreId);

            builder.HasIndex(p => new { p.StoreId, p.Name })
                   .IsUnique()
                   .HasDatabaseName("IX_Product_StoreId_Name");

        }
    }
}
