
using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetFourTodayDishesSpec : Specification<Order>
    {
        public GetFourTodayDishesSpec(int storeId) {

            Query.Where(o => o.StoreId == storeId);
            
            //今天
            var today = DateTimeOffset.UtcNow.Date;
            
            //明天的00:00:00
            var tomorrow = today.AddDays(1);

            Query.Where(o => o.OrderDate >= today && o.OrderDate < tomorrow);
            
            //只拿前四个
            Query.OrderByDescending(o => o.OrderDate)
                 .Take(4);
        }


    }
}
