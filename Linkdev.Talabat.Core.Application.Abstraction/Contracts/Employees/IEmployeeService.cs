using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Employees;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts.Employees
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDto>> GetAllEmployees();

        public Task<EmployeeDto> GetEmployeeById(int id);
    }
}
