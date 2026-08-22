namespace Empo.EmployeeService.Application.AuthService.Role.UpdateRole;

public class UpdateRoleRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
