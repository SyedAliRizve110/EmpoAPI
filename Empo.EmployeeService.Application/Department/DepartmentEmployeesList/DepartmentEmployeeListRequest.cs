using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Department.DepartmentEmployeesList;

public class DepartmentEmployeeListRequest : ModelFilterBase
{
    public Guid DepartmentId { get; set; }
    public string search { get; set; }
}
