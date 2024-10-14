using System.Linq.Expressions;

namespace Linkdev.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withAsNoTracking = true);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool withAsNoTracking = true);

        Task<TEntity?> GetAsync(int id);
        Task<TEntity?> GetAsync(ISpecifications<TEntity,TKey> spec, int id);

        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
