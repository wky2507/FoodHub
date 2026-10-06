using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class GetProductsByStoreId : Specification<Product>
    {
        public GetProductsByStoreId(int storeId) {

            Query.Where(c => c.StoreId == storeId);
        
        }


    }
}
