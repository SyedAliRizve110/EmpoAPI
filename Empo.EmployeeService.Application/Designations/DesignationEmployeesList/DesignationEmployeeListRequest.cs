using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Designations.DesignationEmployeesList;

public class DesignationEmployeeListRequest
 : ModelFilterBase
{
    public Guid DesignationId { get; set; }
    public string search { get; set; }
}
