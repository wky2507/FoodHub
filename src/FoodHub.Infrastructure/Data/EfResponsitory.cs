using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Interface;
using Microsoft.EntityFrameworkCore;
namespace FoodHub.Infrastructure.Data
{
    public class EfReponsitory<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {

        protected readonly AppIdentityDbContext _dbContext;

        public EfReponsitory(AppIdentityDbContext dbContext) : base(dbContext)
        {

            _dbContext = dbContext;
        }


        //    #region 快捷条件查询
        //    public  async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) {

        //        return await _dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);

        //    }

        //    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        //    {

        //       return await _dbContext.Set<T>().AnyAsync(predicate, cancellationToken);

        //    }

        //    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) {

        //        return await _dbContext.Set<T>().CountAsync(predicate, cancellationToken);

        //    }

        //    #endregion

        //    #region 批量操作
        //    //批量添加实体
        public override async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {

            await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return entities;
        }

        //    //批量删除实体
        //    public new async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) {

        //         _dbContext.Set<T>().RemoveRange(entities);

        //        await SaveChangesAsync(cancellationToken);

        //    }
        //} 
        //#endregion

        //菜品数量
      
    }
}
       