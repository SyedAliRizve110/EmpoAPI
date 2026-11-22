using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Leaves.LeaveType;

public interface ILeaveTypeService
{
    Task<LeaveTypeRequestModel> GetAsyncById(Guid leaveTypeId);
    Task<GetLeaveTypeListResponse> ListAsync(GetLeaveTypeListRequest request);
    Task<LeaveTypeDto> AddAsync(CreateLeaveTypeRequest request);
    Task<LeaveTypeDto> UpdateAsync(LeaveTypeRequestModel request);
}
