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

            if (specs.OrderByDesc is not null)
                query = query.OrderByDescending(specs.OrderByDesc);
            else if (specs.OrderBy is not null)
                query = query.OrderBy(specs.OrderBy);

            query = specs.Includes.Aggregate(query, (prev, currentVal) => prev.Include(currentVal));

            return query;
        }
    }
}
