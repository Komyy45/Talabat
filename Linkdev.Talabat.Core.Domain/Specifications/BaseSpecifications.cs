﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Contracts;

namespace Linkdev.Talabat.Core.Domain.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();

        public BaseSpecifications()
        {
            
        }

        public BaseSpecifications(TKey id)
        {
            Criteria = B => B.Id.Equals(id);
        }
    }
}
