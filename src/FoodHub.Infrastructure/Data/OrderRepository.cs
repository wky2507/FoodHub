using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FoodHub.BlazorShared.Dto;
using FoodHub.BlazorShared.Response;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodHub.Infrastructure.Data
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly AppIdentityDbContext _dbContext;

        public OrderRepository(AppIdentityDbContext dbContext):base(dbContext) {

            _dbContext = dbContext;

        }

        //今日营业额
        public async Task<decimal> SumAsync(ISpecification<Order> specification, CancellationToken cancellationToken)
        {
            var query = SpecificationEvaluator.GetQuery(_dbContext.Orders.AsQueryable(), specification);

            var turnover = await query.SumAsync(o => o.TotalPrice);

            Console.WriteLine(turnover);

            return turnover;
        }

        //订单总量
         public override async Task<int> CountAsync(ISpecification<Order> specification, CancellationToken cancellationToken)
        {

            var query = SpecificationEvaluator.GetQuery(_dbContext.Orders.AsQueryable(), specification, evaluateCriteriaOnly: true);

            return await query.CountAsync(cancellationToken);
        }

        public async Task<List<HotSaleDto>> GetHotSaleDishesAsync(int storeId, DateTimeOffset startTime, DateTimeOffset endTime, CancellationToken cancellationToken = default){

            return await _dbContext.Orders.AsTracking()
                .Where(o =>
                    o.StoreId == storeId &&
                    o.OrderDate >= startTime &&
                    o.OrderDate < endTime
                    )
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => oi.ProductId)
                .Select(g => new HotSaleDto {
                    ProductName = g.Select(x => x.Product.Name).FirstOrDefault()!,

                    TotalSaleAmount = g.Sum(x => x.Price),

                    TotalCount = g.Sum(x => x.Count)
                })
                .OrderByDescending(x => x.TotalCount)
                .Take(3)
                .ToListAsync(cancellationToken);

        }

        //public async Task<GetSaleTrendResponse> GetSaleTrendAsync(int storeId, int startTime, int endTime, CancellationToken cancellationToken) {

        //    var query = _dbContext.Orders.AsNoTracking();
            
        //    //我需要起止时间，然后按照两个小时来切
        //    query.Where(o => o.StoreId == storeId);

        //    var distance = startTime - endTime;

        //    var firstTime = startTime;

        //    while (distance > 0) {

        //        query.GroupBy(o => o.OrderDate.Hour >= firstTime && o.OrderDate.Hour < (firstTime + 2))
        //             .Select(g=>)

        //        firstTime += 2;

        //        distance -= 2;
        //    }



        //}

    }
}
