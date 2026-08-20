using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class UserEntity : EntityBase
{
    public string UserName { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    public Guid RoleId { get; set; }
    public User_RoleEntity UserRole { get; set; } = new User_RoleEntity();

    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;
}
