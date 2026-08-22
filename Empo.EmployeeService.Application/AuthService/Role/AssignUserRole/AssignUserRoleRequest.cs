namespace Empo.EmployeeService.Application.AuthService.Role.AssignUserRole;

public class AssignUserRoleRequest
{
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
}
