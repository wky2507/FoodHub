using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetSendingCountSpec :Specification<Order>
    {
        public GetSendingCountSpec(int storeId) {

            Query.Where(o => o.StoreId == storeId && o.Status == OrderStatus.Sending);
        
        }


    }
}
