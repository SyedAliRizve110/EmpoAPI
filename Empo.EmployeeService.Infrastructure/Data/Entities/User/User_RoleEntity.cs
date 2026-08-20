using Empo.BuildingBlocks.Infrastructure.Data;
using System.Data;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class User_RoleEntity : EntityBase
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public UserEntity User { get; set; } = null!;
    public RoleEntity Role { get; set; } = null!;
}
