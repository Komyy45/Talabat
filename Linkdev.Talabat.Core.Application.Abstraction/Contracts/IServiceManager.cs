using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Employees;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IEmployeeService EmployeeService { get; }
    }
}
