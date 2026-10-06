

using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetPendingCountSpec : Specification<Order>
    {
        public GetPendingCountSpec(int storeId) {

            Query.Where(o => o.StoreId == storeId && o.Status == OrderStatus.Pending);
            
        }


    }
}
