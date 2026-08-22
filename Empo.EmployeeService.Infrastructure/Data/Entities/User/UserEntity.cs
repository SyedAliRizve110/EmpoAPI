using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class UserEntity : EntityBase
{
    [MaxLength(100)]
    public string UserName { get; set; }

    [MaxLength(200)]
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }

    [MaxLength(300)]
    public string PasswordHash { get; set; }

    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }

    public bool IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }

    [ForeignKey("RoleId")]
    public Guid? RoleId { get; set; }
    public RoleEntity? Role { get; set; }

    public Guid? EmployeeId { get; set; }
    public EmployeeEntity? Employee { get; set; }
}
