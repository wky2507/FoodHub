
using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Specifications
{
    public class GetOrdersByStoreIdSpec : Specification<Order>
    {
        public GetOrdersByStoreIdSpec(int storeId,int page,int pageSize) {

            Query.Where(c => c.StoreId == storeId).Include(c => c.Buyer).Include(c => c.OrderItems).ThenInclude(c => c.Product);

            var validPage = page < 1 ? 1 : page;

            var validPageSize = pageSize < 10 ? 10 : pageSize;

            var skip = (validPage - 1) * pageSize;

            Query.OrderByDescending(o => o.OrderDate)
                 .Skip(skip)
                 .Take(pageSize);

        }


    }
}
