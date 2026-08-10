using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Attendence.ActiveEmployeesList;

public class ActiveEmployeeListRequest : ModelFilterBase
{
    public string search { get; set; }
}
