using Empo.EmployeeService.Application.AuthService.Role.AssignRolePermission;
using Empo.EmployeeService.Application.AuthService.Role.AssignUserRole;
using Empo.EmployeeService.Application.AuthService.Role.CreateRole;
using Empo.EmployeeService.Application.AuthService.Role.GetRoleDetails;
using Empo.EmployeeService.Application.AuthService.Role.RevokePermission;
using Empo.EmployeeService.Application.AuthService.Role.RoleList;
using Empo.EmployeeService.Application.AuthService.Role.RoleStatus;
using Empo.EmployeeService.Application.AuthService.Role.UpdateRole;

namespace Empo.EmployeeService.Application.AuthService.Intrfaces;

public interface IRoleService
{
    Task<Guid> AddAsync(CreateRoleRequest request);
    Task<Guid> UpdateAsync(UpdateRoleRequest request);
    Task<GetRoleDetailResponse> GetAsync(Guid id);
    Task<bool> HasRoleAssigned(Guid id);
    Task<RoleListResponse> ListAsync(RoleListRequest request);
    Task<Guid> AssignPermission(AssignRolePermissionRequest request);
    Task<Guid> UpdateStatusAsync(RoleStatusRequest request);
    Task<Guid> AssignUserRole(AssignUserRoleRequest request);
    Task<Guid> RevokePermission(RevokePermissionRequest request);
}
