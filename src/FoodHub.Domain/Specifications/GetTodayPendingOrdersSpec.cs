using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetTodayPendingOrdersSpec : Specification<Order>
    {
        public GetTodayPendingOrdersSpec(int storeId) {

            Query.Where(o => o.StoreId == storeId);
            //今天
            var today = DateTime.UtcNow.Date;
            //明天00:00:00
            var tomorrow = today.AddDays(1);

            Query.Where(o => o.Status == OrderStatus.Pending);

            Query.Where(o => o.OrderDate >= today && o.OrderDate < tomorrow);

        }



    }
}
