using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Common;

namespace Linkdev.Talabat.Core.Domain.Entities.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        public required string Name { get; set; }
    }
}
