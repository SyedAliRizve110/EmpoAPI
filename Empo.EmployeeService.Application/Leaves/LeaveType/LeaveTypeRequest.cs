using Empo.BuildingBlocks.Application.SharedModels;

namespace Empo.EmployeeService.Application.Leaves.LeaveType;

public class CreateLeaveTypeRequest
{
    public string Name { get; set; }
    public int DefaultDaysPerYear { get; set; }
    public bool IsPaid { get; set; }
    public bool RequiresApproval { get; set; } = true;
    public bool CarryForwardAllowed { get; set; }
    public int MaxCarryForwardDays { get; set; }
    public bool IsActive { get; set; } = true;
}
public class LeaveTypeRequestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int DefaultDaysPerYear { get; set; }
    public bool IsPaid { get; set; }
    public bool RequiresApproval { get; set; } = true;
    public bool CarryForwardAllowed { get; set; }
    public int MaxCarryForwardDays { get; set; }
    public bool IsActive { get; set; } = true;
}

public class GetLeaveTypeListResponse
{
    public List<LeaveTypeRequestModel> Collection { get; set; }
    public int TotalRecords { get; set; }
}

public class GetLeaveTypeListRequest : ModelFilterBase
{
    public string search { get; set; }
}