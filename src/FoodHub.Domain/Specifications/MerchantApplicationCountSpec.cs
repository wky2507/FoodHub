using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using Ardalis.Specification;
namespace FoodHub.Domain.Specifications
{
    public class MerchantApplicationCountSpec : Specification<MerchantApplication>
    {
        public MerchantApplicationCountSpec(ApplicationStatus? status, string? keyword)
        {

            if (status.HasValue) {
                Query.Where(x => x.Status == status.Value);           
            }

            if (!string.IsNullOrWhiteSpace(keyword)) {

                Query.Where(x =>
                     x.StoreName.Contains(keyword) ||
                     x.ApplicationContact.Any(c =>
                                            c.Name.Contains(keyword) ||
                                            c.Phone.Contains(keyword)));
                     
            }
        }
    }
}
