using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class DefautlDishesPagedSpec : Specification<Product>
    {
        public DefautlDishesPagedSpec(int storeId,int page,int pageSize) {

            Query.Where(c => c.StoreId == storeId);

            var validPage = page < 1 ? 1 : page;

            var validPageSize = pageSize < 10 ? 10 : pageSize;

            var skip = (validPage - 1) * validPageSize;

            Query.OrderByDescending(c => c.CreateAt)
                 .Skip(skip)
                 .Take(validPageSize);
        }


    }
}
