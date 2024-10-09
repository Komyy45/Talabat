using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
