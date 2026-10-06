
using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class GetDishesListCountSpec : Specification<Product>
    {
        public GetDishesListCountSpec(int storeId, string? keywords, bool? isOnSale) {

            Query.Where(c => c.StoreId == storeId);

            if (!string.IsNullOrWhiteSpace(keywords)) {

                Query.Where(c => c.Name.Contains(keywords));
            
            }

            if (isOnSale != null) {

                Query.Where(c => c.IsOnsale == isOnSale);

            }


        }


    }
}
