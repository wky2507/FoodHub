using FoodHub.Domain.Entity.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodHub.Infrastructure.Data.Config
{
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder) {

            var nav = builder.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));

            nav?.SetPropertyAccessMode(PropertyAccessMode.Field);
              
            builder.Property(b => b.IdentityGuid)
                   .IsRequired()
                   .HasMaxLength(100);

        }

    }
}
