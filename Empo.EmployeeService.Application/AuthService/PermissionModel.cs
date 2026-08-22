namespace Empo.EmployeeService.Application.AuthService;

public class PermissionModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public ICollection<Role_PermissionModel> RolePermission { get; set; } = new List<Role_PermissionModel>();
}
