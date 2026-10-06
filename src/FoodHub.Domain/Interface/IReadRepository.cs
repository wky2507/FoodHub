using Ardalis.Specification;

namespace FoodHub.Domain.Interface
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T :class,IAggregateRoot
    {
        //Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        //Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        //Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    }
}
