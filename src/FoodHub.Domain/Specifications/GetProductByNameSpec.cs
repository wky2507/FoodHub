using Ardalis.Specification;


namespace FoodHub.Domain.Specifications
{
    public class GetProductByNameSpec :Specification<Product>
    {
        public GetProductByNameSpec(string name) {

            Query.Where(c => c.Name == name);    

        }



    }
}
