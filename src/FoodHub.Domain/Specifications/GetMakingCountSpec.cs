
using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetMakingCountSpec :Specification<Order>
    {
        public GetMakingCountSpec(int storeId) {

            Query.Where(o => o.StoreId == storeId && o.Status == OrderStatus.Making);
        
        }


    }
}
