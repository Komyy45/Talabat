using System.Linq.Expressions;
using Linkdev.Talabat.Core.Domain.Contracts;

namespace Linkdev.Talabat.Core.Domain.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null!;
        public Expression<Func<TEntity, object>> OrderByDesc { get; set; } = null!;
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecifications()
        {
            
        }

        public BaseSpecifications(TKey id)
        {
            Criteria = B => B.Id.Equals(id);
        }

        protected virtual void AddIncludes()
        {

        }
        protected virtual void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }

        protected virtual void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Take = take;
            Skip = skip;
        }
    }
}
