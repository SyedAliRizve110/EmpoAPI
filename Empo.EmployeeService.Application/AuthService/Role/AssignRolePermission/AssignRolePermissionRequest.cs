namespace Empo.EmployeeService.Application.AuthService.Role.AssignRolePermission;

public class AssignRolePermissionRequest
{
    public Guid RoleId { get; set; }
    public List<Guid> PermissionId { get; set; }
}
