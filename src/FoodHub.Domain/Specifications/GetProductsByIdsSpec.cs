using Ardalis.Specification;
namespace FoodHub.Domain.Specifications
{
    public sealed class GetProductsByIdsSpec : Specification<Product>
    {
        public GetProductsByIdsSpec(IEnumerable<int> productIds) {

            Query.Where(p => productIds.Contains(p.Id));

        }
        
    }
}
