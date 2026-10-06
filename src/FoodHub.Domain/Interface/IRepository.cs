using Ardalis.Specification;
using FoodHub.Domain.Entity.OrderAggregate;

namespace FoodHub.Domain.Interface
{
    public interface IRepository<T> : IRepositoryBase<T> where T: class,IAggregateRoot
    {
        
        new Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        //Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);


    }
}
