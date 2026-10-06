
using Ardalis.Specification;
using FoodHub.Domain.Entity;

namespace FoodHub.Domain.Specifications
{
    public class GetFoodImageByUrlSpec : Specification<Product>
    {
        public GetFoodImageByUrlSpec(string url) {

            Query.Where(c => c.PictureUri == url);

        }
               

    }
}
