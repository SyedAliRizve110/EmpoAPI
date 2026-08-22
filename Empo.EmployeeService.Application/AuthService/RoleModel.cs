namespace Empo.EmployeeService.Application.AuthService;

public class RoleModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // Navigation
    public ICollection<UserModel>? User { get; set; }
    public ICollection<Role_PermissionModel>? Role_Permission { get; set; }
}
