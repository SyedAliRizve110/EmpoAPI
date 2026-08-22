using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.AuthService.Role.RoleList;

public class RoleListRequest : ModelFilterBase
{
    public string search { get; set; }
    public bool isActive { get; set; } = true;
}
