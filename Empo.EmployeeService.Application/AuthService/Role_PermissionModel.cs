namespace Empo.EmployeeService.Application.AuthService;

public class Role_PermissionModel
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public RoleModel Role { get; set; } = null!;
    public PermissionModel Permission { get; set; } = null!;
}
