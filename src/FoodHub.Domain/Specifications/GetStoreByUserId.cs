using Ardalis.Specification;
using FoodHub.Domain.Entity.StoreEntity;
namespace FoodHub.Domain.Specifications
{
    public class GetStoreByUserId : Specification<Store>
    {
        public GetStoreByUserId(string userId) {

            Query.Where(c => c.UserId == userId);

        }

    }
}
