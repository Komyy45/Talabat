using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Contracts;

namespace Linkdev.Talabat.Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(IQueryable<TEntity> query, ISpecifications<TEntity, TKey> specs)
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            if (specs.Criteria is not null)
                query = query.Where(specs.Criteria);

            query = specs.Includes.Aggregate(query, (prev, currentVal) => prev.Include(currentVal));

            return query;
        }
    }
}
