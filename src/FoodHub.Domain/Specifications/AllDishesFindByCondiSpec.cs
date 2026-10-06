
using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class AllDishesFindByCondiSpec :Specification<Product>
    {

        public AllDishesFindByCondiSpec(int storeId,int page,int pageSize,string? keywords,bool? isOnsale) {

            if (!string.IsNullOrWhiteSpace(keywords)) {

                Query.Where(c => c.Name.Contains(keywords));
            
            }

            if (isOnsale != null) {

                Query.Where(c => c.IsOnsale == isOnsale);

            }

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
