using FoodHub.Domain.Entity;
using FoodHub.Domain.Entity.BasketAggregate;
using FoodHub.Domain.Entity.BuyerAggregate;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Entity.StoreEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FoodHub.Infrastructure.Data
{
    public class AppIdentityDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        { }
        
        public DbSet<MerchantApplication> MerchantApplications { get; set; }

        public DbSet<Store> Stores { get; set; }

        //public DbSet<StoreContact> StoreContacts { get; set; }

        public DbSet<ApplicationContact> ApplicationContacts { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<AppUserRefreshToken> AppUserRefreshToken { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);
        }


    }
}
