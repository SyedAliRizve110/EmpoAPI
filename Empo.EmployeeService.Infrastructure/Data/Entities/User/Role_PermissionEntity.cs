using Empo.BuildingBlocks.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class Role_PermissionEntity : EntityBase
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public RoleEntity Role { get; set; }
    public PermissionEntity Permission { get; set; }
}
