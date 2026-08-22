namespace Empo.EmployeeService.Application.AuthService.Role.RevokePermission;

public class RevokePermissionRequest
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}
