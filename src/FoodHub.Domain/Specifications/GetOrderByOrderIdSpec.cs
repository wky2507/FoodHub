

using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetOrderByOrderIdSpec : Specification<Order>
    {
        public GetOrderByOrderIdSpec(int orderId) {

            Query.Where(o => o.Id == orderId).Include(o => o.Buyer).Include(o => o.OrderItems).ThenInclude(c => c.Product);

        }



    }
}
