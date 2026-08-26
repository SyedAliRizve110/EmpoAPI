using Empo.EmployeeService.Application.Branch.AssignBranchEmployee;
using Empo.EmployeeService.Application.Branch.AssignBranchManager;
using Empo.EmployeeService.Application.Branch.BranchEmployeeList;
using Empo.EmployeeService.Application.Branch.CreateBranch;
using Empo.EmployeeService.Application.Branch.ListBranch;

namespace Empo.EmployeeService.Application.Branch.ServiceInterface;

public interface IBranchService
{
    Task<Guid> CreateBranchAsync(CreateBranchRequest request);
    Task<Guid> UpdateBranchAsync(BranchModel request);
    Task<BranchModel> GetBranchDetails(Guid id);
    Task<BranchListResponse> BranchListAsync(BranchListRequest request);
    Task<BranchEmployeeListResponse> BranchEmployeeListAsync(BranchEmployeeListRequest request);
    Task<Guid> AssigBranchEmployee(AssignBranchEmployeeRequest request);
    Task<Guid> AssigBranchManager(AssignBranchManagerRequest request);
    Task<bool> IsBranchExist(Guid id);
}
