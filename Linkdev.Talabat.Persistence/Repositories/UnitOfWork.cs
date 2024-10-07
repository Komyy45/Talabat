using System.Collections.Concurrent;
using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Persistence.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Linkdev.Talabat.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repsoitories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repsoitories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity,TKey>) _repsoitories.GetOrAdd(nameof(TEntity), new GenericRepository<TEntity,TKey>(_dbContext));
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    }
}
