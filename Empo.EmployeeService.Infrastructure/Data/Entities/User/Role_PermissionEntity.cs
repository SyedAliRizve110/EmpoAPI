using Empo.BuildingBlocks.Infrastructure.Data;
using System.Data;
using System.Security;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class Role_PermissionEntity : EntityBase
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public RoleEntity Role { get; set; } = null!;
    public PermissionEntity Permission { get; set; } = null!;
}
