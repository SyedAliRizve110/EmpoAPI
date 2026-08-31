using Empo.BuildingBlocks.Application.Contracts;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Department.ServiceInterface;

namespace Empo.EmployeeService.Application.Department.GetDepartmentDetails;

public class GetDepartmentDetailsQueryHandler : IQueryHandler<GetDepartmentDetailsQuery, GetDepartmentDetailsResponse>
{
    public IDepartmentService _service { get; }

    public GetDepartmentDetailsQueryHandler(IDepartmentService service)
    {
        _service = service;
    }

    public async Task<GetDepartmentDetailsResponse> Handle(GetDepartmentDetailsQuery request, CancellationToken cancellationToken)
    {
        var department = await _service.GetDepartmentDetails(request._departmentId);
        if (department == null)
        {
            throw new NotFoundException("Department", request._departmentId);
        }
        return department;
    }
}
