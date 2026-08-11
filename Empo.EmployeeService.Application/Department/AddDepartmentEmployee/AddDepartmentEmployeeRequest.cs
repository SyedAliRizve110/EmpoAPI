namespace Empo.EmployeeService.Application.Department.AddDepartmentEmployee;

public class AddDepartmentEmployeeRequest
{
    public Guid DepartmentId { get; set; }
    public ICollection<Guid> EmployeesId { get; set; }
}
