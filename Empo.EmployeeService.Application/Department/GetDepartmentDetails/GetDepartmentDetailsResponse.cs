namespace Empo.EmployeeService.Application.Department.GetDepartmentDetails;

public class GetDepartmentDetailsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Guid ManagerId { get; set; }
}
