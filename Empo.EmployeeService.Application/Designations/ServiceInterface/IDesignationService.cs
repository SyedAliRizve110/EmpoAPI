using Empo.EmployeeService.Application.Designations.AssignDesignation;
using Empo.EmployeeService.Application.Designations.CreateDesignation;
using Empo.EmployeeService.Application.Designations.DesignationEmployeesList;
using Empo.EmployeeService.Application.Designations.DesignationList;

namespace Empo.EmployeeService.Application.Designations.ServiceInterface;

public interface IDesignationService
{
    Task<Guid> AddAsync(CreateDesignationRequest request);
    Task<Guid> UpdateAsync(DesignationModel model);
    Task<DesignationModel> GetAsync(Guid Id);
    Task<DesignationListResponse> ListAsync(DesignationListRequest request);
    Task<DesignationEmployeeListResponse> DesignationEmployeeListAsync(DesignationEmployeeListRequest request);
    Task<Guid> AssignDesignationAsync(AssignDesignationRequest request);
}
