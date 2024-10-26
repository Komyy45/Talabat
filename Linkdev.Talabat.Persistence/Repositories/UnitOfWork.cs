using System.Collections.Concurrent;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Persistence.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Linkdev.Talabat.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repsoitories;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repsoitories = new();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var typeName = typeof(TEntity).Name;
            return (IGenericRepository<TEntity,TKey>) _repsoitories.GetOrAdd(typeName, new GenericRepository<TEntity,TKey>(_dbContext));
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    }
}
