using Empo.BuildingBlocks.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class RoleEntity : EntityBase
{
    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public ICollection<UserEntity>? User { get; set; }
    public ICollection<Role_PermissionEntity>? Role_Permission { get; set; }
}
