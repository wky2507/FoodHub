using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetTodayFinishedCountSpec :Specification<Order>
    {
        public GetTodayFinishedCountSpec(int storeId) {

            var utcNow = DateTimeOffset.UtcNow;

            var startTime = new DateTimeOffset(utcNow.Year,utcNow.Month,utcNow.Day,0,0,0,TimeSpan.Zero);

            var endTime = startTime.AddDays(1);

            Query.Where(o => o.OrderDate >= startTime && o.OrderDate < endTime && o.Status == OrderStatus.Completed); 

        }


    }
}
