using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Persistence.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Linkdev.Talabat.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreContext context) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withAsNoTracking)
            => await (withAsNoTracking ? context.Set<TEntity>().AsNoTracking().ToListAsync() :  context.Set<TEntity>().AsNoTracking().ToListAsync());

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
