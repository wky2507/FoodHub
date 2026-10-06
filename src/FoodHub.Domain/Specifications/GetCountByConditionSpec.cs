
using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetCountByConditionSpec :Specification<Order>
    {
        public GetCountByConditionSpec(int storeId, string? keywords, OrderStatus? status, DateTime? dateTime) {

            Query.Where(o => o.StoreId == storeId);

            if (!string.IsNullOrWhiteSpace(keywords)) {

                Query.Where(o => o.OrderNumber.Contains(keywords) || o.Buyer.Name.Contains(keywords) || o.Buyer.Phone.Contains(keywords));
            
            }

            if (status != null) {

                Query.Where(o => o.Status == status);
            
            }

            if (dateTime != null && dateTime != DateTime.MinValue) {

                var utcTime = new DateTime(dateTime.Value.Year,dateTime.Value.Month,dateTime.Value.Day,0,0,0,DateTimeKind.Utc);

                var startTime = new DateTimeOffset(utcTime, TimeSpan.Zero);

                var endTime = startTime.AddDays(1);

                Query.Where(o => o.OrderDate >= startTime && o.OrderDate < endTime);

            }

        }


    }
}
