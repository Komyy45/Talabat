using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Employees;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Employees;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence;
using Linkdev.Talabat.Core.Domain.Entities.Employees;
using Linkdev.Talabat.Core.Domain.Specifications.Employees;

namespace Linkdev.Talabat.Core.Application.Services.Employees
{
    public class EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var spec = new EmployeeSpecifications();

            var employees = await unitOfWork.GetRepository<Employee, int>().GetAllAsync(spec);

            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var spec = new EmployeeSpecifications(id);

            var employee = await unitOfWork.GetRepository<Employee, int>().GetAsync(spec, id);

            return mapper.Map<EmployeeDto>(employee);
        }
    }
}
