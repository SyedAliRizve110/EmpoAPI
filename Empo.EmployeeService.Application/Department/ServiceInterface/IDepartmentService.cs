using Empo.EmployeeService.Application.Department.AddDepartmentEmployee;
using Empo.EmployeeService.Application.Department.AssignManager;
using Empo.EmployeeService.Application.Department.CreateDepartment;
using Empo.EmployeeService.Application.Department.DepartmentEmployeesList;
using Empo.EmployeeService.Application.Department.GetDepartmentDetails;
using Empo.EmployeeService.Application.Department.ListDepartment;
using Empo.EmployeeService.Application.Department.UpdateDepartment;

namespace Empo.EmployeeService.Application.Department.ServiceInterface;

public interface IDepartmentService
{
    Task<Guid> AddDepartment(CreateDepartmentRequest request);
    Task<Guid> UpdateDepartment(UpdateDepartmentRequestModel request);
    Task<GetDepartmentDetailsResponse> GetDepartmentDetails(Guid id);
    Task<GetDepartmentListResponse> DepartmentListAsync(GetDepartmentListRequest request);
    Task<DepartmentEmployeeListResponse> DepartmentEmployeeListAsync(DepartmentEmployeeListRequest request);
    Task<Guid> AddDepartmentEmployeeAsync(AddDepartmentEmployeeRequest request);
    Task<Guid> AssignManagerAsync(AssignManagerRequest request);
}
