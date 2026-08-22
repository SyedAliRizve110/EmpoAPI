using Empo.EmployeeService.Application.AuthService.Permission.CreatePermission;
using Empo.EmployeeService.Application.AuthService.Permission.PermissionList;

namespace Empo.EmployeeService.Application.AuthService.Intrfaces;

public interface IPermissionService
{
    Task<List<string>> GetPermissionsForRoleAsync(Guid? roleId);
    Task<Guid> AddAsync(CreatePermissionRequest request);
    Task<PermissionListResponse> ListAsync(PermissionListRequest request);
}
