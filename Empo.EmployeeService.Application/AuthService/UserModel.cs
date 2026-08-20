namespace Empo.EmployeeService.Application.AuthService;

public class UserModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
}
