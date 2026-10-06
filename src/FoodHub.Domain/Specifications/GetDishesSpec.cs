using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class GetDishesSpec : Specification<Product>
    {
        public GetDishesSpec(int storeId) {

            Query.Where(c => c.StoreId == storeId);

        }


    }
}
