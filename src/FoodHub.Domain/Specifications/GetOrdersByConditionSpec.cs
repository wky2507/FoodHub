using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace FoodHub.Domain.Specifications
{
    public class GetOrdersByConditionSpec : Specification<Order>
    {

        public GetOrdersByConditionSpec(int storeId,int page,int pageSize, string? keywords, OrderStatus? status, DateTime? selectTime,ILogger<Order> logger) {

            Query.Where(o => o.StoreId == storeId).Include(o => o.Buyer).Include(o => o.OrderItems).ThenInclude(o => o.Product);

            //姓名、电话、订单号
            if (!string.IsNullOrWhiteSpace(keywords)) {

                Query.Where(o => o.OrderNumber.Contains(keywords) || o.Buyer.Name.Contains(keywords) || o.Buyer.Phone.Contains( keywords));
            
            }
            if (status != null) {

                Query.Where(o => o.Status == status);

            }
            if (selectTime != null && selectTime != DateTime.MinValue) {

                logger.LogInformation($"打印时间是:{selectTime.Value.AddDays(1)}");

                logger.LogInformation($"前端传来时间{selectTime.Value}");

                var utcDate = new DateTime(selectTime.Value.Year, selectTime.Value.Month, selectTime.Value.Day, 0, 0, 0, DateTimeKind.Utc);

                var startTime = new DateTimeOffset(utcDate, TimeSpan.Zero);

                var endTime = startTime.AddDays(1);

                Query.Where(o => o.OrderDate >= startTime && o.OrderDate < endTime);
                
            }

            var validPage = page < 1 ? 1 : page;

            var validPageSize = pageSize < 10 ? 10 : pageSize;

            var skip = (validPage - 1) * validPageSize;

            Query
                .OrderByDescending(o => o.OrderDate)
                .Skip(skip)
                .Take(validPageSize);
        }


    }
}
