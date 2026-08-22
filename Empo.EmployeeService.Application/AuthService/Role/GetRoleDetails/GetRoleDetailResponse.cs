namespace Empo.EmployeeService.Application.AuthService.Role.GetRoleDetails;

public class GetRoleDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
