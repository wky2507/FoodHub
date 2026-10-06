using Ardalis.Specification;
using FoodHub.BlazorShared.Dto;
using FoodHub.BlazorShared.Response;
using FoodHub.Domain.Entity.OrderAggregate;
namespace FoodHub.Domain.Interface
{
    public interface IOrderRepository: IRepositoryBase<Order>
    {
          Task<List<HotSaleDto>> GetHotSaleDishesAsync(int storeId, DateTimeOffset startTime, DateTimeOffset endTime, CancellationToken cancellationToken);

          Task<decimal> SumAsync(ISpecification<Order> specification, CancellationToken cancellationToken);

        Task<GetSaleTrendResponse> GetSaleTrendAsync(int storeId, DateTimeOffset startTime, DateTimeOffset endTime, CancellationToken cancellationToken) {
            return (Task<GetSaleTrendResponse>)Task.CompletedTask;
        }
    }
}
