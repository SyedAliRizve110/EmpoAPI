using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeEducation.GetEmployeeEducation;

public class GetEmployeeEducationQueryHandler : IQueryHandler<GetEmployeeEducationQuery, List<EmployeeEducationModel>>
{
    public IEmployeeEducationService _service { get; }

    public GetEmployeeEducationQueryHandler(IEmployeeEducationService service)
    {
        _service = service;
    }

    public async Task<List<EmployeeEducationModel>> Handle(GetEmployeeEducationQuery request, CancellationToken cancellationToken)
    {
        var education = await _service.GetEmployeeEducationList(request._employeeId);
        return education;
    }
}
