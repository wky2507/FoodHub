using Ardalis.Specification;
using FoodHub.Domain.Entity.StoreEntity;

namespace FoodHub.Domain.Specifications
{
    public class GetStoreByUserIdSpec :Specification<Store>
    {
        public GetStoreByUserIdSpec(string userId) {
            Query.Where(p => p.UserId == userId);
        }


    }
}
