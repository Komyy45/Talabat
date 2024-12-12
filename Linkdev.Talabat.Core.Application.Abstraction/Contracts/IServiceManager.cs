using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Employees;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Orders;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IEmployeeService EmployeeService { get; }
        public IAuthService AuthService { get; }
        public IOrderService OrderService { get; }
    }
}
