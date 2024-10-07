﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Domain.Common
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public required TKey Id { get; set; }

        public required string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public required string LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
