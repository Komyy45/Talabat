using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Basket;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Employees;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IEmployeeService EmployeeService { get; }
    }
}
