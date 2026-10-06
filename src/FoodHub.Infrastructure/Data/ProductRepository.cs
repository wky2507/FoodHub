using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FoodHub.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodHub.Infrastructure.Data
{
    public class ProductRepository : RepositoryBase<Product>,IProductRepository
    {
        private readonly AppIdentityDbContext _dbContext;

        public ProductRepository(AppIdentityDbContext dbContext):base(dbContext) {

            _dbContext = dbContext;

        }

        public override async Task<int> CountAsync(ISpecification<Product> specification, CancellationToken cancellationToken) {

            var query = SpecificationEvaluator.GetQuery(_dbContext.Products.AsQueryable(), specification, evaluateCriteriaOnly: true);

            return await query.CountAsync(cancellationToken);

        }


    }
}
