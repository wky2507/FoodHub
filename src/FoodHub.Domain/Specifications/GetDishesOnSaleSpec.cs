
using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class GetDishesOnSaleSpec  : Specification<Product>
    {
        public GetDishesOnSaleSpec() {

            Query.Where(p => p.IsOnsale == true);

        }


    }
}
