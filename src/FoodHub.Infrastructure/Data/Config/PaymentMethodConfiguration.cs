using FoodHub.Domain.Entity.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.Infrastructure.Data.Config
{
    public class PaymentMethodConfiguration :IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder) {

            builder.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.HasOne(b => b.Buyer)
                   .WithMany(s => s.PaymentMethods)
                   .HasForeignKey(b => b.BuyerId);
        
        }
    }
}
