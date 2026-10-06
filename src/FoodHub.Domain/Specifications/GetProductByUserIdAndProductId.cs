using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class GetProductByStoreIdAndProductId: Specification<Product>
    {
        public GetProductByStoreIdAndProductId(int storeId, int productId) {

            Query.Where(c => c.StoreId == storeId && c.Id == productId);
        
        }

    }
}
