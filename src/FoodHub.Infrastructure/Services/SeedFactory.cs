using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Interface;

namespace FoodHub.Infrastructure.Services
{
    public class SeedFactory
    {
        public static async Task SeedOrderAsync(IRepository<Order> orderManager,IRepository<Product> productManager) {

            var products = await productManager.ListAsync();

            if (products.Count == 0)
            {
                return;
            }

            var random = new Random();

            for (int i = 1; i <= 20; i++) {

                var order = CreateRandomOrder(products, random, i);

                await orderManager.AddAsync(order);

            }
        
        }
        public static Order CreateRandomOrder(IReadOnlyList<Product> products, Random random, int index) {

            var order = new Order($"FH20260930{index:0000}", DateTimeOffset.UtcNow.AddMinutes(random.Next(0, 60 * 24 * 7)), random.Next(4, 7), 2, (OrderStatus)random.Next(0, 7), "江苏省苏州市昆山市玉山镇前进中路恒泰商务大厦", true, "不要辣", 4.5m, 6);

            var items = new List<OrderItem>();

            int itemCount = random.Next(1, 4);
            
            //每个订单应该给1-3条item
            for (int i = 0; i < itemCount; i++) {

                var product = products[random.Next(products.Count)];

                int quantity = random.Next(1, 4);

                items.Add(new OrderItem(order.Id, product.Id, quantity, product));

            }

            order.AddOrderItems(items);

            return order;
        }


    }
}
