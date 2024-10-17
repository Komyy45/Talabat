using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Basket;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Employees;
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
        private readonly Lazy<IBasketService> _basketService;


        public IProductService ProductService => _productService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;

        public IBasketService BasketService => _basketService.Value;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, Func<IBasketService> basketServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService(this._unitOfWork, this._mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(this._unitOfWork, this._mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory);
        }

    }
}
