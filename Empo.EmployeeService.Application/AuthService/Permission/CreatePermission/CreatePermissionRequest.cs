namespace Empo.EmployeeService.Application.AuthService.Permission.CreatePermission;

public class CreatePermissionRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
