using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Persistence.Data;
using Microsoft.Extensions.Logging;

namespace Linkdev.Talabat.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreContext context) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withAsNoTracking = true)
        {

            if (typeof(TEntity) == typeof(Product))
                return (IEnumerable<TEntity>) await (withAsNoTracking ? context.Set<Product>().Include(p => p.Brand).Include(p => p.Category).AsNoTracking().ToListAsync() 
                    : context.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync());
           
            return await (withAsNoTracking ? context.Set<TEntity>().AsNoTracking().ToListAsync() : context.Set<TEntity>().ToListAsync());
        }
           

        public async Task<TEntity?> GetAsync(int id)
            => await context.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => context.Set<TEntity>().Update(entity);   

        public void Delete(TEntity entity)
            => context.Set<TEntity>().Remove(entity);
    }
}
