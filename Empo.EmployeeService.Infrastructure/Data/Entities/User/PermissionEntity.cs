using Empo.BuildingBlocks.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class PermissionEntity : EntityBase
{
    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public ICollection<Role_PermissionEntity> RolePermission { get; set; }
}
