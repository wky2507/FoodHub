using FoodHub.Domain.Entity;

using Ardalis.Specification;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
namespace FoodHub.Domain.Specifications
{
    public class MerchantApplicationByUserIdSpec:Specification<MerchantApplication>
    {

        public MerchantApplicationByUserIdSpec(string userId) {

            Query.Where(x => x.UserId == userId);
        }


    }
}
