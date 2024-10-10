using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Employees;

namespace Linkdev.Talabat.Core.Domain.Specifications.Employees
{
    public class EmployeeSpecifications : BaseSpecifications<Employee, int>
    {
        public EmployeeSpecifications()
        {
            Includes.Add(E => E.Department!);
        }

        public EmployeeSpecifications(int id) : base(id) 
        {
            Includes.Add(E => E.Department!);
        }
    }
}
