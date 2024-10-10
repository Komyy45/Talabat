namespace Linkdev.Talabat.Core.Domain.Entities.Employees
{
    public class Employee : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public int? Age { get; set; }

        public double Salary { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
    }
}
