using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Department.ListDepartment;

public class GetDepartmentListRequest : ModelFilterBase
{
    public string search { get; set; }
}