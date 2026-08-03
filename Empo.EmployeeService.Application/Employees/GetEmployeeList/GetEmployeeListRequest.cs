using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Employees.GetEmployeeList;

public class GetEmployeeListRequest : ModelFilterBase
{
    public string search { get; set; }
}
