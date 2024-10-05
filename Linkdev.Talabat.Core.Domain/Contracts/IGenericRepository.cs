using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey> 
        where TKey : IEquatable<TKey>   
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withAsNoTracking);

        Task<TEntity?> GetAsync(int id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
