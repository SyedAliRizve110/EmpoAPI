using Empo.EmployeeService.Application.Employees.EmployeesModel;

namespace Empo.EmployeeService.Application.Department;

public class DepartmentModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Guid ManagerId { get; set; }
}
