
namespace FoodHub.Domain.Entity.OrderAggregate
{
    public class OrderItem :BaseEntity
    {
        public int OrderId { get; private set; }

        public Order Order { get; private set; }

        public int ProductId { get;private set; }

        public Product Product { get; private set; }

        public int Count { get; private set; }

        public decimal Price { get; private set; }

        //商品快照
        public string OrderTimeProductName { get; private set; }

        public decimal OrderTimeProductPrice { get; private set; }

        #pragma warning disable
        private OrderItem() { }

        public OrderItem(int orderId, int productId, int count, Product product):this()
        {
            
            OrderId = orderId;

            ProductId = productId;

            Count = count;

            Product = product;

            Price = Count * Product.Price;

            OrderTimeProductName = product.Name;

            OrderTimeProductPrice = product.Price;
        }

    }
}
