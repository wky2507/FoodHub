using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using Ardalis.Specification;

namespace FoodHub.Domain.Specifications
{
    public class MerchantApplicantionPagedSpec : Specification<MerchantApplication>
    {

        public MerchantApplicantionPagedSpec(int page,int pageSize,ApplicationStatus? status, string? keyword) {

            Query.AsNoTracking()
                 .AsSplitQuery()
                 .Include(x => x.ApplicationContact);

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

            int validPage = page < 1 ? 1 : page;

            int validPageSize = pageSize < 1 ? 10 : pageSize;

            int skip = (validPage - 1) * validPageSize;

            Query.OrderByDescending(x => x.SubmittedAt)
                 .Skip(skip)
                 .Take(validPageSize);

        }
    }
}
