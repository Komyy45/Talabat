using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Basket;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Employees;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Orders;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Products;
using Linkdev.Talabat.Core.Application.Services.Employees;
using Linkdev.Talabat.Core.Application.Services.Products;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;

namespace Linkdev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IOrderService> _orderService;


        public IProductService ProductService => _productService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IAuthService AuthService => _authService.Value;
        public IOrderService OrderService => _orderService.Value;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,Func<IOrderService> orderServiceFactory, Func<IAuthService> authServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService(this._unitOfWork, this._mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(this._unitOfWork, this._mapper));
            _authService = new Lazy<IAuthService>(authServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _orderService = new Lazy<IOrderService>(orderServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        
        }

    }
}
