
using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
   public class GetOrdersTotalCountByStoreId :Specification<Order>
    {
        public GetOrdersTotalCountByStoreId(int storeId) {

            Query.Where(b => b.StoreId == storeId);
        
        }


    }
}
