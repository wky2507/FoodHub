using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetTodayOrdersSpec : Specification<Order>
    {

        public GetTodayOrdersSpec(int storeId) {

            Query.Where(o => o.StoreId == storeId);

            //今天 00:00:00
            var today = DateTimeOffset.UtcNow.Date;

            //第二天00:00:00
            var tomorrow = today.AddDays(1);

            Query.Where(c => c.OrderDate >= today && c.OrderDate < tomorrow);

        }
    }
}
