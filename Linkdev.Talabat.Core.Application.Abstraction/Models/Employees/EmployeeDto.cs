using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int? Age { get; set; }

        public double Salary { get; set; }

        public int? DepartmentId { get; set; }

        public string? Department { get; set; }
    }
}
