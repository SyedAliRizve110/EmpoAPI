namespace Empo.EmployeeService.Application.Department.CreateDepartment;

public class CreateDepartmentRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Guid? ManagerId { get; set; }
}
