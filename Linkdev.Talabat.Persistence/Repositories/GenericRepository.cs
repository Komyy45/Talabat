using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Persistence.Data;

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

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool withAsNoTracking = true)
        {
            var query = ApplySpecifications(context.Set<TEntity>(), spec);

            if (withAsNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
          
        public async Task<TEntity?> GetAsync(int id)
        {
            if(typeof(TEntity) == typeof(Product))
                return await context.Set<Product>().Include(p => p.Brand).Include(p => p.Category).AsNoTracking().FirstOrDefaultAsync() as TEntity;

            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> spec, int id)
        {
            return await ApplySpecifications(context.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
            => await context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => context.Set<TEntity>().Update(entity);   

        public void Delete(TEntity entity)
            => context.Set<TEntity>().Remove(entity);

        #region Helpers

        private IQueryable<TEntity> ApplySpecifications(IQueryable<TEntity> query, ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationEvaluator.GetQuery(query, spec);
        }

        #endregion
    }
}
