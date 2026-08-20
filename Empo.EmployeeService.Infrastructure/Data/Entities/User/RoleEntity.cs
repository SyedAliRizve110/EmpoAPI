using Empo.BuildingBlocks.Infrastructure.Data;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class RoleEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // Navigation
    public ICollection<User_RoleEntity> UserRole { get; set; } = new List<User_RoleEntity>();
    public ICollection<Role_PermissionEntity> Role_Permission { get; set; } = new List<Role_PermissionEntity>();
}
