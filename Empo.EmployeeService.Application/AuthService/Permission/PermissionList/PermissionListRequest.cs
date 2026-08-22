using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.AuthService.Permission.PermissionList;

public class PermissionListRequest : ModelFilterBase
{
    public string search { get; set; }
}
