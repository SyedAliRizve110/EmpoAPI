using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Designations.DesignationList;

public class DesignationListRequest : ModelFilterBase
{
    public string search { get; set; }
}
