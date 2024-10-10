using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
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


        public IProductService ProductService => _productService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService(this._unitOfWork, this._mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(this._unitOfWork, this._mapper));
        }

    }
}
