﻿using System.Linq.Expressions;

namespace Linkdev.Talabat.Core.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; set; }

        public Expression<Func<TEntity, object>> OrderBy { get; set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get; set; }
    }
}
