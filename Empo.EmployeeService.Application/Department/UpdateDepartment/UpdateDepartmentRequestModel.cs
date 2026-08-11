namespace Empo.EmployeeService.Application.Department.UpdateDepartment;

public class UpdateDepartmentRequestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Guid ManagerId { get; set; }
}
