using Empo.BuildingBlocks.Infrastructure.Data;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class PermissionEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public ICollection<Role_PermissionEntity> RolePermission { get; set; } = new List<Role_PermissionEntity>();
}
